namespace CS_semester_3.part2; 

public class Worker : Person {
    private int _salary;
    public int Salary {
        get => _salary;
        set {
            if (value < 0)
                throw new Exception("Зарплата не должна быть отрицательной");
            _salary = value;
        }
    }

    public override string ToString() {
        return base.ToString() + ", Worker: " + Salary;
    }
    
    public Worker() : base("default_name") {
        Salary = 0;
    }
    
    public Worker(int salary, string name) : base(name) {
        Salary = salary;
    }
}