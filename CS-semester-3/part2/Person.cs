namespace CS_semester_3.part2; 

public abstract class Person {
    public string Name { get; }

    public override string ToString() {
        return Name;
    }

    protected Person(string name) {
        Name = name;
    }
}