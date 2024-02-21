using System.Runtime.InteropServices;

namespace CS_semester_3; 

public class VirtualMemory : IDisposable, IAsyncDisposable 
{
    private const int Offset = 2;
    private readonly int _bufferSize;
    private readonly int _pageLength;
    private readonly int _pageSize;
    
    private readonly Stream _fileStream;
    
    private Page[] Buffer { get; }
    
    private long Length { get; }
    
    private void SavePage(Page page) 
    {
        _fileStream.Position = page.Index * _pageSize + Offset;
        _fileStream.Write(page.BitMap, 0, page.BitMap.Length);
        _fileStream.Write(MemoryMarshal.AsBytes(new ReadOnlySpan<int>(page.Data)));
        page.IsModified = false;
    }
    
    private long? GetBufferPageIndex(long index) 
    {
        if ((index >= Length) || (index < 0))
            return null;

        try 
        {
            var pageIndex = index / _pageLength;

            if (Buffer.Any(page => page.Index == pageIndex)) 
            {
                return pageIndex;
            }

            var oldestPage = Buffer[0];
            var oldestPageIndex = 0;

            for (var i = 1; i < _bufferSize; i++) 
            {
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
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
            return null;
        }
        
    }
    
    public bool GetElement(int index, out int result) 
    {
        var pageIdx = GetBufferPageIndex(index) ?? -1;

        result = 0;
        
        if (pageIdx == -1) 
            return false;

        if (Buffer[pageIdx].BitMap[index % _pageLength] != 1)
            return false;
        
        result = Buffer[pageIdx].Data[index % _pageLength];
        return true;
    }
    
    public bool SetElement(int index, int item) 
    {
        var pageIdx = GetBufferPageIndex(index) ?? -1;

        if (pageIdx == -1)
            return false;
        
        Buffer[pageIdx].BitMap[index % _pageLength] = 1;
        Buffer[pageIdx].Data[index % _pageLength] = item;
        Buffer[pageIdx].IsModified = true;
        
        return true;
    }
    
    public VirtualMemory(long length, string filename = "./data.bin", int bufferSize = 4, int pageLength = 128) 
    {
        Length = length;
        _bufferSize = bufferSize;
        _pageLength = pageLength;
        _pageSize = pageLength + pageLength * sizeof(int);

        var pageCount = (long)Math.Ceiling((decimal)Length / _pageLength);
        var size = pageCount * _pageSize;
        
        if (!File.Exists(filename)) 
        {
            _fileStream = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);
            _fileStream.Write("VM"u8 );

            var empty = new byte[size];
            _fileStream.Write(empty);
        }
        else 
            _fileStream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
        
        var temp = new Page(pageCount, Array.Empty<byte>(), Array.Empty<int>()) { LastRequest = new DateTime(0) };
        Buffer = Enumerable.Repeat(temp, _bufferSize).ToArray();
        
        for (var i = 0; i < _bufferSize; i++) 
            GetBufferPageIndex(i * _pageLength);
    }

    private void SaveAll() 
    {
        foreach (var page in Buffer) 
        {
            if (page.IsModified) {
                SavePage(page);
            }
        }
    }
    
    public void Dispose() 
    {
        SaveAll();
        
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync() 
    {
        SaveAll();

        await _fileStream.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    ~VirtualMemory() 
    {
        _fileStream.Dispose();
    }
}