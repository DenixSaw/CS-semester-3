namespace CS_semester_3.part2; 

public class TestFunction2 {
    public static bool Run() {
        List<Person> list = new();

        try {
            list.Add(new Official("Minister", "Vladimir"));
            list.Add(new Pupil("AVT-213", "Maxim"));
            list.Add(new Worker(25000, "Dmitriy"));
            list.Add(new Turner(5, 80000, "Valeriy"));
            list.Add(new Student(2, "ФИТ-213", "Daniil"));
            list.Add(new PartTimeStudent("denis@nstu.ru", 2, "АВТ-213", "Denis"));
            list.Add(new Programmer("ReactJS", 250000, "Sergey"));
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return false;
        }
        

        foreach (var item in list) {
            Console.WriteLine(item);
        }
        
        return true;
    }
}