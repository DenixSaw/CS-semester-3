namespace CS_semester_3;



public class Date {
    private int _day;
    private int[] _daysByMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    
    public int Day {
        get => _day;
        set {
            if (value < 1 || value > 31)
                throw new Exception("Арина");
            if (value > _daysByMonth[_month - 1])
                throw new Exception("Арина 4");
            
            _day = value;
        }
    }

    private int _month;
    
    public int Month {
        get => _month;
        set {
            if (value < 1 || value > 12)
                throw new Exception("Арина 2");
            _month = value;
        }
    }
    
    private int _year;
    
    public int Year {
        get => _year;
        set {
            if (value < 0)
                throw new Exception("Арина 3");
            _year = value;
        }
    }

    public Date(int day, int month, int year) {
        Year = year;
        Month = month;
        Day = day;
    }

    public void Print() {
        Console.WriteLine($"{_day}/{_month}/{_year}");
    }
}