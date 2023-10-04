namespace CS_semester_3;

public enum SystemType {
    FAT, ext4, None
}

public class Partition {
    private string _name = "Partition";
    public string Name {
        get =>_name;
        set => _name = value;
    }

    private ulong _start;
    public ulong Start {
        get => _start; 
        private set => _start = value;
        
    }

    private ulong _end = 1;
    public ulong End {
        get => _end;
        private set {
            if (value <= _start)
                throw new ArgumentException("Индекс конечного байта должен быть после старта");
            _end = value;
        }
    }

    private SystemType _systemType = SystemType.FAT;
    public SystemType SystemType {
        get => _systemType;
        set {
            _memoryUsed = 0;
            _systemType = value;
        }
    }

    private ulong _memoryUsed;
    public ulong MemoryUsed {
        get => _memoryUsed;
        set {
            if (value > _end - _start + 1)
                throw new ArgumentException("Раздел не может использовать больше отведенной ему памяти");
            _memoryUsed = value;
        }
    }

    public void Print(int idx = -1) {
        Console.WriteLine((idx != -1 ? ("\nИнформация о разделе " + idx + ":") : "\nИнформация о разделе:"));
        Console.WriteLine("Название: " + Name);
        Console.WriteLine("Номер начального байта: " + Start);
        Console.WriteLine("Номер конечного байта: " + End);
        Console.WriteLine("Тип системы: " + SystemType);
        Console.WriteLine("Использованной памяти: " + MemoryUsed);
    }
    
    public Partition(string name, ulong start, ulong end, SystemType systemType, ulong memoryUsed) {
        Name = name;
        Start = start;
        End = end;
        SystemType = systemType;
        MemoryUsed = memoryUsed;
    }
}
