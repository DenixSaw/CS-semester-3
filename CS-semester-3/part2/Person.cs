namespace CS_semester_3.part2; 

public abstract class Person {
    public string Name { get; }

    public override string ToString() {
        return "Person: " + Name;
    }

    public Person(string name) {
        Name = name;
    }
}