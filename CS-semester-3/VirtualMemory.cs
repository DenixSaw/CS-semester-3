using System.Runtime.InteropServices;

namespace CS_semester_3; 

/// <summary>
/// Класс, управляющий имитацией виртуальной памяти.
/// </summary>
public class VirtualMemory : IDisposable, IAsyncDisposable 
{
    private const int Offset = 2;
    private readonly int _bufferSize;
    private readonly int _pageLength;
    private readonly int _pageSize;
    
    private readonly Stream _fileStream;
    
    private Page[] Buffer { get; }
    
    private long Length { get; }
    
    /// <summary>
    /// Метод сохранения данных страницы в файл.
    /// </summary>
    private void SavePage(Page page) 
    {
        // Смещение указателя в файле на место, где расположены битовая карта и данные страницы, согласно индексу.
        _fileStream.Position = page.Index * _pageSize + Offset;
        // Запись битовой карты страницы.
        _fileStream.Write(page.BitMap, 0, page.BitMap.Length);
        // Запись данных страницы.
        _fileStream.Write(MemoryMarshal.AsBytes(new ReadOnlySpan<int>(page.Data)));
        // Перевод флага, отвечающего за изменение страницы в буфере. Т.к. страница записана в файл.
        page.IsModified = false;
    }
    
    /// <summary>
    /// Получение индекса страницы в буфере по индексу элемента.
    /// </summary>
    private long? GetBufferPageIndex(long index) 
    {
        if ((index >= Length) || (index < 0))
            return null;

        try 
        {
            // Считаем абсолютный индекс страницы.
            var pageIndex = index / _pageLength;

            // Если в буфере уже есть страница, с таким абсолютным индексом, вовзращаем ее отсносительный индекс.
            for (var i = 0; i < _bufferSize; i++)
                if (Buffer[i].Index == pageIndex)
                    return i;

            // Ищем самую старую страницу в буфере для замены.
            var oldestPage = Buffer[0];
            var oldestPageIndex = 0;

            for (var i = 1; i < _bufferSize; i++) 
            {
                if (Buffer[i].LastRequest >= oldestPage.LastRequest) continue;
                oldestPage = Buffer[i];
                oldestPageIndex = i;
            }

            // Если самая старая страница была изменена, сохраняем ее в файл.
            if (oldestPage.IsModified) 
                SavePage(oldestPage);

            
            // Переменные, чтобы считать данные из файла.
            var bitMap = new byte[_pageLength];
            var dataBytes = new byte[_pageLength * sizeof(int)];

            _fileStream.Position = pageIndex * _pageSize + Offset;

            _fileStream.ReadExactly(bitMap);
            _fileStream.ReadExactly(dataBytes);

            var data = MemoryMarshal.Cast<byte, int>(dataBytes).ToArray();

            // Запись в буфер новой страницы и возврат ее индекса.
            Buffer[oldestPageIndex] = new Page(pageIndex, bitMap, data);
            return oldestPageIndex;
        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
            return null;
        }
        
    }
    
    /// <summary>
    ///  Получение значения элемента по индексу.
    /// </summary>
    public bool GetElement(int index, out int result) 
    {
        // Получаем индекс страницы, на которой находится элемент с переданным индексом.
        var bufferPageIndex = GetBufferPageIndex(index) ?? -1;

        result = 0;
        
        if (bufferPageIndex == -1) 
            return false;
        
        if (Buffer[bufferPageIndex].BitMap[index % _pageLength] != 1)
            return false;
        
        result = Buffer[bufferPageIndex].Data[index % _pageLength];
        return true;
    }
    
    /// <summary>
    /// Установка значения элемента по переданному индексу.
    /// </summary>
    public bool SetElement(int index, int item) 
    {
        // Получаем индекс страницы, на которой находится элемент с переданным индексом.
        var bufferPageIndex = GetBufferPageIndex(index) ?? -1;
        
        if (bufferPageIndex == -1)
            return false;
        
        Buffer[bufferPageIndex].BitMap[index % _pageLength] = 1;
        Buffer[bufferPageIndex].Data[index % _pageLength] = item;
        Buffer[bufferPageIndex].IsModified = true;
        
        return true;
    }
    
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public VirtualMemory(long length, string filename = "./data.bin", int bufferSize = 4, int pageLength = 128) 
    {
        Length = length;
        _bufferSize = bufferSize;
        _pageLength = pageLength;
        _pageSize = pageLength + pageLength * sizeof(int);
        
        var pageCount = (long)Math.Ceiling((decimal)Length / _pageLength);
        var size = pageCount * _pageSize;
        
        // Проверка существования файла. Если файл не существует, он будет создан и заполнен нулями, согласно количеству переданной памяти.
        if (!File.Exists(filename)) 
        {
            _fileStream = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);
            _fileStream.Write("VM"u8 );

            var empty = new byte[size];
            _fileStream.Write(empty);
        }
        else 
            _fileStream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
        
        // Временная переменная, представляющая собой объект класса страницы. Заполненной нулями.
        var temp = new Page(pageCount, Array.Empty<byte>(), Array.Empty<int>()) { LastRequest = new DateTime(0) };
        // Далее в буфер записывается количество страниц равное размеру буфер.
        Buffer = Enumerable.Repeat(temp, _bufferSize).ToArray();
        
        // Для случая, когда в файле уже есть записи проходимся в цикле и записываем в буффер первые несколько записей, пока в буфере есть место.
        for (var i = 0; i < _bufferSize; i++) 
            GetBufferPageIndex(i * _pageLength);
    }
    
    /// <summary>
    /// Сохранение всех страниц из буфера в файл.
    /// </summary>
    private void SaveAll() 
    {
        foreach (var page in Buffer) 
        {
            // Если страница изменялась за то время, что была в буфере, то мы её сохраняем. Иначе этого можно не делать.
            if (page.IsModified) {
                SavePage(page);
            }
        }
    }
    
    /// <summary>
    /// Реализация метода Dispose.
    /// </summary>
    public void Dispose() 
    {
        SaveAll();
        
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Реализация асинхронного Dispose.
    /// </summary>
    public async ValueTask DisposeAsync() 
    {
        SaveAll();

        await _fileStream.DisposeAsync();
        GC.SuppressFinalize(this);
    }
    
    /// <summary>
    /// Деструктор оъекта класса. Закрывает поток записи в файл и вызывает Dispose метод.
    /// </summary>
    ~VirtualMemory() 
    {
        Dispose();
    }
}