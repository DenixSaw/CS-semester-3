namespace CS_semester_3.part2; 

public class Worker : Person {
    public int Salary { get; set; }

    public override string ToString() {
        return Name + " " + Salary;
    }
}