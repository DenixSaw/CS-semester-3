namespace CS_semester_3; 

public class VirtualMemory : IDisposable, IAsyncDisposable {
    private const int Offset = 2;
    private const int BufferSize = 4;
    private const int BitMapSize = 128;
    private const int DataSize = BitMapSize * sizeof(int);
    private const int PageSize = BitMapSize + DataSize;
    private readonly Stream _fileStream;
    
    public string Filename { get; set; }
    public Page[] Buffer { get; }
    
    public long Length { get; }
    public long Size { get; }
    
    private void SavePage(Page page) {
        _fileStream.Position = page.Index * PageSize + Offset - 1;

        using var binaryWriter = new BinaryWriter(_fileStream);
        foreach (var value in page.BitMap)
            binaryWriter.Write(value);
        
        foreach (var value in page.Data) 
            binaryWriter.Write(value);
    }
    
    private long GetPage(long index) {
        if (index >= Length || index < 0)
            throw new IndexOutOfRangeException("Некорректный индекс");
        
        var pageIndex = index / PageSize;
        
        if (Buffer.Any(page => page.Index == pageIndex)) {
            return pageIndex;
        }

        var oldestPage = Buffer[0];
        var oldestPageIndex = 0;

        for (var i = 1; i < BufferSize; i++) {
            if (Buffer[i].UpdatedAt >= oldestPage.UpdatedAt) continue;
            oldestPage = Buffer[i];
            oldestPageIndex = i;
        }

        if (oldestPage.IsModified)
            SavePage(oldestPage);
            
        var bitMap = new byte[BitMapSize];

        const int dataLength = DataSize / sizeof(int);
        var data = new int[dataLength];
        
        _fileStream.Position = index * PageSize + Offset - 1;
        using var binaryReader = new BinaryReader(_fileStream);
        
        for (var i = 0; i < BitMapSize; i++) 
            bitMap[i] = binaryReader.ReadByte();
        
        for (var i = 0; i < dataLength; i++) 
            data[i] = binaryReader.ReadInt32();

        Buffer[oldestPageIndex] = new Page(pageIndex, bitMap, data);
        return oldestPageIndex;
    }
    
    public bool GetElement(int index, out int result) {
        var idx = GetPage(index);

        if (Buffer[idx].BitMap[index % BitMapSize] != 1)
            throw new NullReferenceException("Элемент не существует");
        
        result = Buffer[idx].Data[index % BitMapSize];
        return true;
    }
    
    public bool SetElement(int index, int item) {
        var idx = GetPage(index);
        Buffer[idx].BitMap[index % BitMapSize] = 1;
        Buffer[idx].Data[index % BitMapSize] = item;
        return true;
    }
    
    public VirtualMemory(long length, string filename = "./data.bin") {
        Filename = filename;
        Length = length;
        Size = length * sizeof(int) + Offset;
        
        _fileStream = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);
        
        if (!File.Exists(filename)) {
            using StreamWriter writer = new(_fileStream);
        
            writer.Write('V');
            writer.Write('M');

            Size += PageSize - Size % PageSize;
            for (long i = 0; i < Size; i++)
                writer.Write('\0');
        }

        Buffer = new Page[BufferSize];

        for (var i = 0; i < BufferSize; i++) 
            GetElement(i * PageSize, out _);
    }

    public void Dispose() {
        foreach (var page in Buffer) 
            SavePage(page);
        
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync() {
        foreach (var page in Buffer) 
            SavePage(page);

        await _fileStream.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    ~VirtualMemory() {
        _fileStream.Dispose();
    }
}