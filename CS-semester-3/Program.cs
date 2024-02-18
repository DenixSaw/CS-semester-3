namespace CS_semester_3;

internal static class Program {
    private static void Main() {
        using var vm = new VirtualMemory(21, "./data.bin", 4, 4);
        
        var setStatus = vm.SetElement(0, 1);
        if (!setStatus)
            Console.WriteLine("Не удалось записать элемент");
        
        var getStatus = vm.GetElement(0, out var n);
        Console.WriteLine(getStatus ? n : "Не удалось получить элемнт");
    }
}