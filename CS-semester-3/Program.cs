namespace CS_semester_3;

internal class Program {
    private static void Main() {
        using var vm = new VirtualMemory(21, "./data.bin", 4, 4);
        // vm.SetElement(0, 1);

        try {
            vm.GetElement(0, out var t);
            Console.WriteLine(t);
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
        }
        
    }
}