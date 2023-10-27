using System.Collections.ObjectModel;
using System.Collections.Specialized;

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

        // Queue<int> q = new();
        //
        // for (var i = 0; i < 5; i++)
        //     q.Enqueue(i);
        //
        // Console.WriteLine("Очередь:");
        // foreach (var item in q) 
        //     Console.WriteLine(item);
        //
        // Console.WriteLine("Удаляем 2 элемента:");
        // for (var i = 0; i < 2; i++)
        //     q.Dequeue();
        // foreach (var item in q) 
        //     Console.WriteLine(item);
        //
        // Console.WriteLine("Добавляем 1");
        //
        // q.Enqueue(1);
        //
        // Console.WriteLine("Формируем list:");
        // var list = q.ToList();
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

        // Random random = new();
        // var partitionCount = 0;
        //
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
        // Console.WriteLine("Очередь:");
        // foreach (var item in q) 
        //     item.Print();
        //
        // Console.WriteLine("\n\nУдаляем 2 элемента:");
        // for (var i = 0; i < 2; i++)
        //     q.Dequeue();
        // foreach (var item in q) 
        //     item.Print();
        //
        // Console.WriteLine("\n\nДобавляем 1 раздел:");
        // q.Enqueue(GenerateNewPartition());
        // foreach (var item in q) 
        //     item.Print();
        //
        //
        // var list = q.ToList();
        //
        // Console.WriteLine("\n\nФормируем list:");
        // foreach (var item in list) 
        //     item.Print();
        //
        // Console.WriteLine("\n\nВведите искомое значение названия:");
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
        // Console.WriteLine("\nРезультат сравнения раздела p с list[0]: " + p?.CompareTo(list[0]));
        // Console.WriteLine("Результат сравнения раздела p с k: " + p?.Compare(p, k));
        
        
         var obs = new ObservableCollection<Partition>();
         
         obs.CollectionChanged += Logger;
         
         obs.Add(new("p1", 0, 10, SystemType.ext4, 5));
         obs.Add(new("p2", 11, 15, SystemType.None, 0));
         
         obs.RemoveAt(0);
    }

    private static void Logger(object? sender, NotifyCollectionChangedEventArgs eventArgs) {
        if (eventArgs.NewItems != null) {
            Console.WriteLine("\n\nДобавлен элемент");
            (eventArgs.NewItems[0] as Partition)?.Print();
        }
        else {
            Console.WriteLine("\n\nЭлемент удален");
            (eventArgs.OldItems?[0] as Partition)?.Print();
        }
        
    }
}
