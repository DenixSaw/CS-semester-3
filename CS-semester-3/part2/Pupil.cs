namespace CS_semester_3.part2; 

public class Pupil : Person {
    public string Group { get; set; }

    public Pupil() : base("Default_name") {
        Group = "avt-213";
    }

    public Pupil(string group, string name) : base(name) {
        Group = group;
    }
    
    public override string ToString() {
        return base.ToString() + ", Pupil: " + Group;
    }
}