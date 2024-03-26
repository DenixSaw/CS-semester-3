namespace CS_semester_3;

// Точка входа в программу.
internal static class Program 
{
    // Тестирующая функция.
    private static void Main() 
    {
        using var vm = new VirtualMemory(10000);
        
        if (!vm.SetElement(0, 255))
            Console.WriteLine("Не удалось записать элемент 0");
        
        if (!vm.SetElement(1024, 123))
            Console.WriteLine("Не удалось записать элемент 1024");
        
        if (!vm.SetElement(2048, 18))
            Console.WriteLine("Не удалось записать элемент 2048");
        
        var getStatus = vm.GetElement(1024, out var n);
        Console.WriteLine(getStatus ? n : "Не удалось получить элемeнт");
        
        var status = vm.GetElement(2048, out var k);
        Console.WriteLine(status ? k : "Не удалось получить элемeнт");
    }
}