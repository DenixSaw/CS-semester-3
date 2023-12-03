namespace CS_semester_3.part2; 

public sealed class Official : Person {
    public string Position { get; set; }
    public Official(string position, string name) : base(name) {
        Position = position;
    }
    
    public override string ToString() {
        return base.ToString() + ", Official: " + Position;
    }
}