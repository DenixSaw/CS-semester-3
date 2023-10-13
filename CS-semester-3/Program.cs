using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace CS_semester_3;

internal class Program {
    private static void Main() {
        // ArrayList list = new();
        Random random = new();
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

        // Queue<int> q = new();
        //
        // for (var i = 0; i < 5; i++)
        //     q.Enqueue(i);
        //
        // foreach (var item in q) 
        //     Console.WriteLine(item);
        //
        // List<int> list = q.ToList();
        // foreach (var item in list) 
        //     Console.WriteLine(item);
        //
        // Console.WriteLine("Введите искомое значение:");
        // if (!int.TryParse(Console.ReadLine(), out var itemToFind))
        //     itemToFind = 0;
        // for (var i = 0; i < list.Count; i++) {
        //     if (list[i] != itemToFind) continue;
        //     Console.WriteLine("Индекс: " + i);
        //     break;
        // }

        // var partitionCount = 0;
        // Partition GenerateNewPartition() {
        //     var start = (ulong)random.NextInt64(250);
        //     var end = start + (ulong)random.NextInt64(250);
        //     return new Partition("Partition" + ++partitionCount, start, end, SystemType.ext4, (ulong)random.NextInt64(0, (long)
        //         (end - start + 1)));
        // }
        //
        // Queue<Partition> q = new();
        //
        // for (var i = 0; i < 5; i++)
        //     q.Enqueue(GenerateNewPartition());
        //
        // foreach (var item in q) 
        //     item.Print();
        //
        // List<Partition> list = q.ToList();
        // foreach (var item in list) 
        //     item.Print();
        //
        // Console.WriteLine("Введите искомое значение названия:");
        // var name = Console.ReadLine();
        //
        // for (var i = 0; i < list.Count; i++) {
        //     if (list[i].Name != name) continue;
        //     Console.WriteLine("Индекс: " + i);
        //     break;
        // }
        //
        // var p = list[0].Clone() as Partition;
        // var k = list[1].Clone() as Partition;
        // Console.WriteLine("Результат сравнения раздела p с list[0]: " + p?.CompareTo(list[0]));
        // Console.WriteLine("Результат сравнения раздела p с k: " + p?.Compare(p, k));
        //
        
         var obs = new ObservableCollection<Partition>();

         obs.CollectionChanged += Logger;
         
         obs.Add(new("p1", 0, 10, SystemType.ext4, 5));
         obs.Add(new("p2", 11, 15, SystemType.None, 0));
    }

    private static void Logger(object? sender, NotifyCollectionChangedEventArgs eventArgs) {
        Console.WriteLine("Добавлен элемент");
        (eventArgs.NewItems?[0] as Partition)?.Print();
    }
}
