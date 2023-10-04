namespace CS_semester_3;

public enum Type {
    HDD, SSD
}

public enum TableType {
    GPT, MBR, NONE
}

public class Disk {
    private readonly List<Partition> _partitions = new();
    public IReadOnlyList<Partition> Partitions => _partitions;

    private string _manufacturer = "Manufacturer";
    public string Manufacturer {
        get => _manufacturer; 
        set {
            if (value.Length < 1)
                throw new ArgumentException("Производитель не должен быть пустым");
            _manufacturer = value;
        }
    }

    private string _model = "Model";
    public string Model {
        get => _model;
        set {
            if (value.Length < 1)
                throw new ArgumentException("Название модели не должно быть пустым");
            _model = value;
        }
    }

    private string _serialNumber = "0";
    public string SerialNumber {
        get =>_serialNumber;
        set {
            if (value.Length < 1)
                throw new ArgumentException("Серийный номер не должен быть пустым");
            _serialNumber = value;
        }
    }

    public Type Type { get; set; }

    private ulong _capacity;
    public ulong Capacity {
        get => _capacity;
        set {
            if (value < 1)
                throw new ArgumentException("Объем не должен быть меньше 1");
            _capacity = value;
        }
    }

    public void AddPartition(Partition partition) {
        if (partition.End > _capacity)
            throw new ArgumentException("Раздел превышает объем диска");
        if (_partitions.Any(p => !(partition.Start > p.End || partition.End < p.Start))) {
            throw new ArgumentException("Разделы пересекаются");
        }
        _partitions.Add(partition);
    }

    public void RemovePartition(int idx) {
        if (idx < 0 || idx > Partitions.Count - 1)
            throw new ArgumentException("Раздела по такому индексу не существует");
        _partitions.RemoveAt(idx);
    }

    public TableType TableType { get; set; }

    public void Print() {
        Console.WriteLine("\nИнформация о диске:");
        Console.WriteLine("Производитель: " + Manufacturer);
        Console.WriteLine("Модель: " + Model);
        Console.WriteLine("Серийный номер: " + SerialNumber);
        Console.WriteLine("Тип: " + Type);
        Console.WriteLine("Объем: " + Capacity);
        Console.WriteLine("Тип таблицы: " + TableType);
    }
    
    public Disk(string manufacturer, string model, string serialNumber, Type type, ulong capacity, TableType tableType) {
        Manufacturer = manufacturer;
        Model = model;
        SerialNumber = serialNumber;
        Type = type;
        Capacity = capacity;
        TableType = tableType;
    }
}

