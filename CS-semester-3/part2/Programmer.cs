namespace CS_semester_3.part2;

public class Programmer : Worker {
    public string Framework { get; set; }

    public override string ToString() {
        return Name + " " + Salary + " " + Framework;
    }
}