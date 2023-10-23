namespace CS_semester_3.part2; 

public class Student : Pupil {
    public int Year { get; set; }

    public override string ToString() {
        return Name + " " + Group + " " + Year;
    }
}