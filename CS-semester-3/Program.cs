namespace CS_semester_3;

internal static class Program {
    private static void Main() {
        using var vm = new VirtualMemory(21, "./data.bin", 4, 4);
        
        if (!vm.SetElement(4, 42))
            Console.WriteLine("Не удалось записать элемент");
        
        if (!vm.SetElement(0, 256))
            Console.WriteLine("Не удалось записать элемент");
        
        if (!vm.SetElement(20, int.MaxValue))
            Console.WriteLine("Не удалось записать элемент");
        
        var getStatus = vm.GetElement(0, out var n);
        Console.WriteLine(getStatus ? n : "Не удалось получить элемнт");
    }
}