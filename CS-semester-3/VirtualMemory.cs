namespace CS_semester_3; 

public class VirtualMemory : IDisposable, IAsyncDisposable {
    private const int Offset = 2;
    private const int BufferSize = 4;
    private const int PageSize = 512 + 128;
    private readonly Stream _fileStream;
    
    public string Filename { get; set; }
    public Page[] Buffer { get; }
    public long Size { get; }

    private int GetPage(int index) {
        if (index > Size || index < 0)
            throw new IndexOutOfRangeException("Некорректный индекс");
        
        var pageIndex = index / PageSize;
        
        if (Buffer.Any(page => page.Index == pageIndex)) {
            return pageIndex;
        }

        //Эту строчку убрать
        return pageIndex;
        //TODO: Сделать получение самой старой страницы
        //var oldestPageIndex = Buffer.Aggregate()
    }
    
    public bool GetElement(int index, out int result) {

        result = 4;
        return true;
    }
    
    public VirtualMemory(long size, string filename = "./data.bin") {
        Filename = filename;
        Size = size;
        
        if (File.Exists(filename)) {
            _fileStream = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);
        }
        else {
            _fileStream = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);

            using StreamWriter writer = new(_fileStream);
        
            writer.Write('V');
            writer.Write('M');

            size -= (size - Offset) % PageSize + Offset;
            for (long i = 0; i < size; i++)
                writer.Write('\0');
        }

        Buffer = new Page[BufferSize];

        // for (var i = 0; i < 3; i++) {
        //     
        // }
    }

    public void Dispose() {
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync() {
        await _fileStream.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    ~VirtualMemory() {
        _fileStream.Dispose();
    }
}