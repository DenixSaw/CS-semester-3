namespace CS_semester_3;

internal class Program {
    private static void Main() {
        Password x = new("abcd");
        Password y = new("abcd");
        Password z = new("Abcd123");
        
        Console.WriteLine("Равенство 1 и 2 (Equals): " + Equals(x, y));
        Console.WriteLine("Равенство 1 и 3 (Equals): " + Equals(x, z));
        Console.WriteLine("Равенство 1 и 2: " + (x == y));
        Console.WriteLine("Неравенство 1 и 3: " + (x != y));
        
        Console.WriteLine("Hash code 1: " + x.GetHashCode());

        x -= 't';
        Console.WriteLine("Новый пароль 1: " + x);
        
        Console.WriteLine("Пароль 1 > 2: " + (x > y));
        Console.WriteLine("Пароль 3 > 2: " + (z > y));
        
        Console.WriteLine("Равенство 1 и 2: " + (x == y));
        Console.WriteLine("Стандартный пароль: " + ++x);

        Console.WriteLine(x ? "Пароль 1 сильный" : "Пароль 1 слабый");
        
        Console.WriteLine(y ? "Пароль 2 сильный" : "Пароль 2 слабый");

        var str = "Abcdefg";
        Console.WriteLine($"\nСтрока {str}:");
        Console.WriteLine($"С выделенной серединой {str.HighlightMiddle()}");
        Console.WriteLine($"Корректная ли длина пароля: {str.ValidPasswordLength()}");
        
        var shortStr = "abcd";
        Console.WriteLine($"Строка {shortStr}:");
        try {
            Console.WriteLine($"С выделенной серединой {shortStr.HighlightMiddle()}");
        }
        catch (Exception e) {
            Console.WriteLine("Ошибка: " + e.Message);
        }

        Console.WriteLine($"Корректная ли длина пароля: {shortStr.ValidPasswordLength()}");
    }
}
