namespace CS_semester_3;

internal class Program {
    private static void Main() {
        Disk disk;
        string manufacturer = "", model = "", serialNumber = "";
        int diskType = -1, tableType = -1;
        ulong capacity = 0;
        
        while (true) {
            if (manufacturer?.Length < 1) {
                Console.WriteLine("Введите производителя: ");
                manufacturer = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            if (model?.Length < 1) {
                Console.WriteLine("Введите модель: ");
                model = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            if (serialNumber?.Length < 1) {
                Console.WriteLine("Введите серийный номер: ");
                serialNumber = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            if (diskType < 0) {
                Console.WriteLine("Введите тип диска (0 - HDD, 1 - SSD):");
                if (!int.TryParse(Console.ReadLine(), out diskType) || diskType < 0)
                    diskType = 0;
            }
            
            if (tableType < 0) {
                Console.WriteLine("Введите тип таблицы (0 - GPT, 1 - MDR, 2 - None):");
                if (!int.TryParse(Console.ReadLine(), out tableType) || tableType < 0)
                    tableType = 0;
            }
            
            if (capacity < 1) {
                Console.WriteLine("Введите объем диска > 0:");
                if (!ulong.TryParse(Console.ReadLine(), out capacity))
                    capacity = 1;
            }

            try {
                disk = new Disk(manufacturer, model, serialNumber, (Type)(diskType % 2), capacity, (TableType)(tableType % 3));
                break;
            } catch (ArgumentException e) { 
                Console.WriteLine("\nОшибка: " + e.Message);
            }
        }
        
        
        Console.WriteLine("\nВведите количество разделов: ");
        if (!int.TryParse(Console.ReadLine(), out var partitionCount))
            partitionCount = 0;

        for (var i = 0; i < partitionCount; i++) {
            var name = "";
            var systemType = -1;

            while (true) {
                if (name?.Length < 1) {
                    Console.WriteLine("\nВведите название раздела: ");
                    name = Console.ReadLine()?.Trim() ?? string.Empty;
                }

                if (systemType < 0) {
                    Console.WriteLine("Введите тип системы (0 - FAT, 1 - ext4, 3 - None):");
                    if (!int.TryParse(Console.ReadLine(), out systemType) || systemType < 0)
                        systemType = 0;
                }
            
                Console.WriteLine("Введите номер начального байта:");
                if (!ulong.TryParse(Console.ReadLine(), out var start))
                    start = 0;
                
                Console.WriteLine("Введите номер конечного байта:");
                if (!ulong.TryParse(Console.ReadLine(), out var end))
                    end = 0;
                
                Console.WriteLine("Введите объем использованной памяти > 0:");
                if (!ulong.TryParse(Console.ReadLine(), out var memoryUsed))
                    memoryUsed = 1;
                

                try {
                    var partition = new Partition(name, start, end, (SystemType)systemType, memoryUsed);
                    disk.AddPartition(partition);
                    break;
                } catch (ArgumentException e) { 
                    Console.WriteLine("\nОшибка: " + e.Message);
                }
            }
        }
        
        
        disk.Print();
        for (var i = 0; i < disk.Partitions.Count; i++) 
            disk.Partitions[i].Print(i + 1);
        
        if (partitionCount < 1) return;
        
        Console.WriteLine("\nВведите номер раздела, который нужно удалить: ");
        if (!int.TryParse(Console.ReadLine(), out var deleteIdx))
            deleteIdx = 1;
        
        try {
            disk.RemovePartition(deleteIdx - 1);
        } catch (ArgumentException e) {
            Console.WriteLine("\nОшибка: " + e.Message);
        }
        
        if (partitionCount > 1)
            disk.Partitions[0].SystemType = SystemType.None;
        
        Console.WriteLine("Обновленные разделы: ");
        for (var i = 0; i < disk.Partitions.Count; i++) 
            disk.Partitions[i].Print(i + 1);
    }
}
