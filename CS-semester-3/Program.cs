using System.Collections;

namespace CS_semester_3;

internal class Program {
    private static void Main() {
        // ArrayList list = new();
        // Random random = new();
        // for (var i = 0; i < 5; i++) 
        //     list.Add(random.Next(100));
        // list.Add("AVT-213");
        //
        // Console.WriteLine($"Количество элементов {list.Count}. Элементы:");
        // foreach (var item in list)
        //     Console.WriteLine(item);
        //
        // Console.WriteLine("Введите удаляемое значение:");
        // var itemToDelete = Console.ReadLine();
        // for (var i = 0; i < list.Count; i++) {
        //     if (list[i]?.ToString() != itemToDelete) continue;
        //     list.RemoveAt(i);
        //     break;
        // }
        //
        // foreach (var item in list)
        //     Console.WriteLine(item);
        //
        // Console.WriteLine("Введите значение, которое хотите найти:");
        // var itemToFind = Console.ReadLine();
        //
        // for (var i = 0; i < list.Count; i++) {
        //     if (list[i]?.ToString() != itemToFind) continue;
        //     Console.WriteLine("Индекс: " + i);
        //     break;
        // }

        Queue<int> q = new();

        for (var i = 0; i < 5; i++)
            q.Enqueue(i);

        foreach (var item in q) 
            Console.WriteLine(item);

        List<int> list = q.ToList();
        foreach (var item in list) 
            Console.WriteLine(item);
        
        Console.WriteLine("Введите искомое значения:");
        if (!int.TryParse(Console.ReadLine(), out var itemToFind))
            itemToFind = 0;
        for (var i = 0; i < list.Count; i++) {
            if (list[i] != itemToFind) continue;
            Console.WriteLine("Индекс: " + i);
            break;
        }

    }
}
