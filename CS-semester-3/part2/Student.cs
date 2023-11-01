namespace CS_semester_3.part2; 

public class Student : Pupil {
    private int _year;
    public int Year {
        get => _year;
        set {
            if (value < 1 || value > 9)
                throw new Exception("Некорректное значение курса");
            _year = value;
        }
    }

    public override string ToString() {
        return base.ToString() + " " + Year;
    }

    public Student(): base("AVT-213", "default_name") {
        Year = 1;
    }
    
    public Student(int year, string group, string name): base(group, name) {
        Year = year;
    }
}