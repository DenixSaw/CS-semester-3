using CS_semester_3.part2;

namespace CS_semester_3;

internal class Program {
    private static void Main() {
        if (TestFunction.Run()) {
            Console.WriteLine("Первая тестовая функция выполнена успешно\n\n");
        }

        if (TestFunction2.Run()) {
            Console.WriteLine("Вторая тестовая функция выполнена успешно\n\n");
        }
        
    }
}
