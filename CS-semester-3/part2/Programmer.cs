namespace CS_semester_3.part2;

public class Programmer : Worker {
    private const int ProfesstionId = 42;
    public string Framework { get; set; }

    public Programmer(string framework, int salary, string name) : base(salary, name) {
        Framework = framework;
    }
    
    public override string ToString() {
        return base.ToString() + " " + Framework;
    }

    public override bool Equals(object? obj) {
        if (obj is Programmer p) return Framework == p.Framework;
        return false;
    }
    
    public override int GetHashCode() {
        return ProfesstionId.GetHashCode();
    }
}