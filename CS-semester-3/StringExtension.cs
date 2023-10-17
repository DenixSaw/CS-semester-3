namespace CS_semester_3;

public static class StringExtension {
    public static string HighlightMiddle(this string str) {
        var middle = str.Length / 2;
        if (str.Length % 2 == 0)
            throw new Exception("Строка четной длины");
        return str[..middle] + str[middle].ToString().ToUpper() + str[(middle + 1)..];
    }

    public static bool ValidPasswordLength(this string str) {
        return str.Length >= 6 && str.Length <= 12;
    }
}