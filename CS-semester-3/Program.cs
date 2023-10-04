namespace CS_semester_3;

internal class Program {
    private static void Main() {
        var operations = new[] { "+", "-", "*", "/" };
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Clear();

        while (true) {
            Console.WriteLine("Введите операцию(+,-,*,/, 0):");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
                break;
            var op = key.KeyChar.ToString();

            if (op == "0")
                break;

            if (!operations.Contains(op))
                continue;
            
            double a, b;
            string input;
            
            do {
                Console.WriteLine("\nВведите первое число:");
                input = Console.ReadLine() ?? string.Empty;
            } while (!double.TryParse(input, out a));

            do {
                Console.WriteLine("Введите второе число:");
                input = Console.ReadLine() ?? string.Empty;
            } while (!double.TryParse(input, out b));

            switch (op) {
                case "+":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Calculator.Add(a, b);
                    break;
                case "-":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Calculator.Substract(a, b);
                    break;
                case "*":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Calculator.Multiply(a, b);
                    break;
                case "/":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Calculator.Divide(a, b);
                    break;
            }
        }
    }
}
