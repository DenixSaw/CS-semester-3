namespace CS_semester_3;

internal class Program {
    private static void PrintMatrix(int[,] matrix, int n, int m) {
        for (var i = 0; i < n; i++) {
            for (var k = 0; k < m; k++) {
                Console.Write($"{matrix[i, k]}\t");
            }
            Console.WriteLine();
        }
    }
    
    private static void FillMatrix(int[,] matrix, int start, int n, int m, int offset) {
        if (start < 0) start = 0;
        if (start > n / 2 || start > m / 2) return;

        var offsetX = m - start;
        var offsetY = n - start;
        
        for (var i = start; i < offsetX; i++) {
            if (matrix[start, i] == 0)
                matrix[start, i] = offset + i - start + 1;
            if (matrix[offsetY - 1, m - i - 1] == 0) 
                matrix[offsetY - 1, m - i - 1] = offset + (n - start * 2) + (m - start * 2) - 1 + i - start;
        }
        
        for (var i = start; i < offsetY; i++) {
            if (matrix[i, offsetX - 1] == 0)
                matrix[i, offsetX - 1] = offset + offsetX + i - (start * 2);
            if (matrix[n - i - 1, start] == 0)
                matrix[n - i - 1, start] = offset + (m - start * 2) * 2 + (n - start * 2) - 2 + i - start;
        }

        FillMatrix(matrix, start + 1, n, m, offset + (n - start * 2) * 2 + (m - start * 2) * 2 - 4);
    }
    
    private static void Main() {
        //const a = new Array(+prompt()).fill(0).map((_, i) => i + 1)
        //Array = [i for i in range(0, int(input())+1)]
        
        Console.WriteLine("Введите число > 0:");
        if (!int.TryParse(Console.ReadLine(), out var firstN) || firstN < 1)
            firstN = 1;
        var firstTask = new int[firstN];
        for (var i = 0; i < firstN; i++) 
            firstTask[i] = i + 1;

        for (var i = firstN - 1; i >= 0; i--)
            Console.WriteLine(firstTask[i]);
        Console.WriteLine();
        
        Console.WriteLine("Введите размерность:");
        if (!int.TryParse(Console.ReadLine(), out var secondN) || secondN < 1)
            secondN = 1;

        var matrix = new int[secondN, secondN];

        for (var i = 0; i < secondN; i++) 
            for (var k = 0; k < secondN; k++) 
                matrix[i, k] = i - k > 1 ? 0 : 1;

        PrintMatrix(matrix, secondN, secondN);


        Console.WriteLine("Введите n >= 2:");
        if (!int.TryParse(Console.ReadLine(), out var n) || n < 2)
            n = 2;
        Console.WriteLine("Введите m >= 2:");
        if (!int.TryParse(Console.ReadLine(), out var m) || m < 2)
            m = 2;

        var a = new int[n, m];

        FillMatrix(a, 0, n, m, 0);

        PrintMatrix(a, n, m);
    }
}
