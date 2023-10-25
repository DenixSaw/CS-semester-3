namespace CS_semester_3;

internal class Program {
    private static void Print<T>(IEnumerable<T> c) {
        foreach (var item in c) {
            if (item is Date) {
                (item as Date)?.Print();
            }
            else {
                Console.WriteLine(item);
            }
        }
        Console.WriteLine("");
    }
    private static void Main() {
        // string[] months = {
        //     "January", "February", "March", "April", "May", "June", "July", "August", "September",
        //     "October", "November", "December"
        // };
        // var n = 4;
        // var lengthIsFour = from m in months where m.Length == n select m;
        // Print(lengthIsFour);
        //
        // var sumwin = from m in months where m[0] == 'J' || m[0] == 'F' || m[0] == 'A' && m.Length == 6 || m[0] == 'D' select m;
        // Print(sumwin);
        //
        // Print(from m in months orderby m select m);
        //
        // Print(from m in months where m.Length > 3 && m.Contains('u') select m);

        var list = new List<Date> { new(16, 10, 2018), new(16, 11, 2016), new(16, 10, 2016), new(28, 9, 2021), new(29, 8, 2022) };
        Print(from date in list where date.Year == 2016 select date);
        Print(from date in list where date.Month == 10 select date);
        var x = from date in list orderby date.Year, date.Month, date.Day select date;
        Print(from date in list where date.Day is > 20 and < 30 select date);
        x.LastOrDefault()?.Print();
        Print(x);
        (from date in list where date.Day == 16 orderby date.Year, date.Month select date).FirstOrDefault()?.Print();
    }
}
