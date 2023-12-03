namespace CS_semester_3.part1;

public static class Extensions {
    public static string HighlightMiddle(this string str) {
        var middle = str.Length / 2;
        if (str.Length % 2 == 0)
            throw new Exception("Строка четной длины");
        return str[..middle] + str[middle].ToString().ToUpper() + str[(middle + 1)..];
    }

    public static bool ValidPasswordLength(this Password str) {
        return str.Length is >= 6 and <= 12;
    }
}