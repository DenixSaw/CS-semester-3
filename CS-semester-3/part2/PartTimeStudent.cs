namespace CS_semester_3.part2; 

public class PartTimeStudent : Student {
    public string Login { get; set; }

    public PartTimeStudent(string login, int year, string group, string name) : base(year, group, name) {
        Login = login;
    }

    public override void PrintName() {
        Console.WriteLine($"Студент заочного отделения {Name}");
    }
    
    public override string ToString() {
        return base.ToString() + ", PartTimeStudent: " + Login;
    }
}