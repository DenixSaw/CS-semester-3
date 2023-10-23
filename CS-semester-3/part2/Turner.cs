namespace CS_semester_3.part2; 

public class Turner : Worker {
    public int Experience { get; set; }

    public override string ToString() {
        return Name + " " + Salary + " " + Experience;
    }
}