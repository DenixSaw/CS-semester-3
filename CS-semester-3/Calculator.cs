namespace CS_semester_3; 

public class Calculator {
    public static void Add(double x, double y) {
        Console.WriteLine($"{x} + {y} = {x + y}");
    }

    public static void Multiply(double x, double y) {
        Console.WriteLine($"{x} * {y} = {x * y}");
    }

    public static void Divide(double x, double y) {
        Console.WriteLine($"{x} / {y} = {x / y}");
    }

    public static void Substract(double x, double y) {
        Console.WriteLine($"{x} - {y} = {x - y}");
    }
}