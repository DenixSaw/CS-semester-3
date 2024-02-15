using System.Runtime.InteropServices;

namespace CS_semester_3; 

public class VirtualMemory : IDisposable, IAsyncDisposable {
    private const int Offset = 2;
    private readonly int _bufferSize;
    private readonly int _pageLength;
    private readonly int _pageSize;
    
    private readonly Stream _fileStream;
    
    private Page[] Buffer { get; }
    
    private long Length { get; }
    
    private void SavePage(Page page) {
        _fileStream.Position = page.Index * _pageSize + Offset;
        _fileStream.Write(page.BitMap, 0, page.BitMap.Length);
        _fileStream.Write(MemoryMarshal.AsBytes(new ReadOnlySpan<int>(page.Data)));
        page.IsModified = false;
    }
    
    private long GetPage(long index) {
        if (index >= Length || index < 0)
            throw new IndexOutOfRangeException("Некорректный индекс");
        
        var pageIndex = index / _pageLength;
        
        if (Buffer.Any(page => page.Index == pageIndex)) {
            Buffer[pageIndex].LastRequest = DateTime.Now;
            return pageIndex;
        }

        var oldestPage = Buffer[0];
        var oldestPageIndex = 0;

        for (var i = 1; i < _bufferSize; i++) {
            if (Buffer[i].LastRequest >= oldestPage.LastRequest) continue;
            oldestPage = Buffer[i];
            oldestPageIndex = i;
        }

        if (oldestPage.IsModified) 
            SavePage(oldestPage);
        
            
        var bitMap = new byte[_pageLength];
        var dataBytes = new byte[_pageLength * sizeof(int)];
        
        _fileStream.Position = pageIndex * _pageSize + Offset;
        
        _fileStream.ReadExactly(bitMap);
        _fileStream.ReadExactly(dataBytes);

        var data = MemoryMarshal.Cast<byte, int>(dataBytes).ToArray();
        
        Buffer[oldestPageIndex] = new Page(pageIndex, bitMap, data);
        return oldestPageIndex;
    }
    
    public bool GetElement(int index, out int result) {
        var idx = GetPage(index);
        
        if (Buffer[idx].BitMap[index % _pageLength] != 1)
            throw new NullReferenceException("Элемент не существует");
        
        result = Buffer[idx].Data[index % _pageLength];
        return true;
    }
    
    public bool SetElement(int index, int item) {
        var bufferIdx = GetPage(index);
        
        Buffer[bufferIdx].BitMap[index % _pageLength] = 1;
        Buffer[bufferIdx].Data[index % _pageLength] = item;
        Buffer[bufferIdx].IsModified = true;
        
        return true;
    }
    
    public VirtualMemory(long length, string filename = "./data.bin", int bufferSize = 4, int pageLength = 128) {
        Length = length;
        _bufferSize = bufferSize;
        _pageLength = pageLength;
        _pageSize = pageLength + pageLength * sizeof(int);

        var pageCount = (long)Math.Ceiling((decimal)Length / _pageLength);
        var size = pageCount * _pageSize + Offset;
        
        if (!File.Exists(filename)) {
            _fileStream = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);
            _fileStream.Write(new []{ (byte)'V'} );
            _fileStream.Write(new []{ (byte)'M'} );

            var empty = new byte[size];
            for (var i = 0; i < size; i++)
                empty[i] = 0;
            
            _fileStream.Write(empty);
        }
        else {
            _fileStream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
        }
        
        
        var temp = new Page(pageCount, Array.Empty<byte>(), Array.Empty<int>()) { LastRequest = new DateTime(0) };
        Buffer = Enumerable.Repeat(temp, _bufferSize).ToArray();
        
        for (var i = 0; i < _bufferSize; i++) {
            GetPage(i * _pageLength);
        }
    }

    public void SaveAll() {
        foreach (var page in Buffer) {
            if (page.IsModified) {
                SavePage(page);
            }
        }
    }
    
    public void Dispose() {
        SaveAll();
        
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync() {
        SaveAll();

        await _fileStream.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    ~VirtualMemory() {
        _fileStream.Dispose();
    }
}