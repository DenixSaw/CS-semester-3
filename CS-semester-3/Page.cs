namespace CS_semester_3; 

public struct Page {
    public int Index { get; }
    public bool IsModified { get; set; } = false;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool[] BitMap { get; }
    public  int[] Data { get; }

    public Page(int index, bool[] bitMap, int[] data) {
        Index = index;
        BitMap = bitMap;
        Data = data;
    }
}