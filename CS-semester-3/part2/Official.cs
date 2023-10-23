namespace CS_semester_3.part2; 

public sealed class Official : Person {
    public Official(string name, string position) : base(name) {
        Position = position;
    }

    public string Position { get; set; }

    public override string ToString() {
        return Name + " " + Position;
    }
}