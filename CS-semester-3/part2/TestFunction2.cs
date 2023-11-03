namespace CS_semester_3.part2; 

public class TestFunction2 {
    public static bool Run() {
        List<Person> list = new();
        var student = new Student(2, "AVT-213", "Igor");
        var partTimeStudent = new PartTimeStudent("denis@nstu.ru", 2, "АВТ-213", "Denis");
        
        try {
            list.Add(new Official("Minister", "Vladimir"));
            list.Add(new Pupil("AVT-213", "Maxim"));
            list.Add(new Worker(25000, "Dmitriy"));
            list.Add(new Turner(5, 80000, "Valeriy"));
            list.Add(student);
            list.Add(partTimeStudent);
            list.Add(new Programmer("ReactJS", 250000, "Sergey"));
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return false;
        }
        

        foreach (var item in list) {
            Console.WriteLine(item);
        }
        
        Console.WriteLine("\nПроверка перегрузки виртуального метода PrintName");
        student.PrintName();
        partTimeStudent.PrintName();
        
        return true;
    }
}