namespace CS_semester_3; 

public class Page {
    public long Index { get; }
    public bool IsModified { get; set; }
    public DateTime LastRequest { get; set; } = DateTime.Now;
    public byte[] BitMap { get; }
    public  int[] Data { get; }

    public Page(long index, byte[] bitMap, int[] data) {
        if (index < 0)
            throw new ArgumentException("Индекс не может быть отрицательным");
        
        Index = index;
        BitMap = bitMap;
        Data = data;
    }
}