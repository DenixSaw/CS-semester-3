namespace CS_semester_3.part2; 

public class Pupil : Person {
    public string Group { get; set; }

    public override string ToString() {
        return Name + " " + Group;
    }
}