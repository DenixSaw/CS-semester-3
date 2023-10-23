namespace CS_semester_3.part2; 

public class PartTimeStudent : Student {
    public string Login { get; set; }
    
    public override string ToString() {
        return Name + " " + Group + " " + Year + " " + Login;
    }
}