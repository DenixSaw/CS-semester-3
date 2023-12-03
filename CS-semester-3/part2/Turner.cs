namespace CS_semester_3.part2; 

public class Turner : Worker {
    private int _expirience;

    public int Experience {
        get => _expirience;
        set {
            if (value < 0 || value > 120)
                throw new Exception("Некорректное значение стажа");
            _expirience = value;
        }
    }

    public Turner(int experience, int salary, string name) : base(salary, name) {
        Experience = experience;
    }
    
    public override string ToString() {
        return base.ToString() + ", Turner: " + Experience;
    }
}