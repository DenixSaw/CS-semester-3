namespace CS_semester_3 {
    internal class Program {
        static void PrintMatrix(int[,] matrix, int n, int m) {
            for (var i = 0; i < n; i++) {
                for (var k = 0; k < m; k++) {
                    Console.Write($"{matrix[i, k]}\t");
                }
                Console.WriteLine();
            }
        }

        // private static void FillMatrix(int[,] matrix, int start, int n, int offset) {
        //     if (start > n / 2)
        //         return;
        //     
        //     var innerOffset = n - start;
        //     for (var i = start; i < innerOffset; i++) {
        //         matrix[start, i] = offset + i - start + 1;
        //         matrix[i, innerOffset - 1] = offset + innerOffset + i - (start * 2);
        //         matrix[innerOffset - 1, n - i - 1] = offset + (n - start * 2) * 2 - 1 + i - start;
        //         if (matrix[n - i - 1, start] == 0)
        //             matrix[n - i - 1, start] = offset + (n - start * 2) * 3 - 2 + i - start;
        //     }
        //     
        //     FillMatrix(matrix, start + 1, n, offset + (n - start * 2) * 4 - 4);
        // }
        
        private static void FillMatrix(int[,] matrix, int start, int n, int m, int offset) {
            if (start > n / 2)
                return;

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
            //const a = new Array(+prompt()).fill(0).map((_, i) => i + 1);
            //Array = [i for i in range(0, int(input())+1)]
            //Console.WriteLine("Введите число > 0:");
            //if (!int.TryParse(Console.ReadLine(), out int firstN) || n < 0)
            //    firstN = 0;
            //int[] firstTask = new int[firstN];
            //for (int i = 0; i < firstN; i++) 
            //    firstTask[i] = i + 1;

            //for (int i = firstN - 1; i >= 0; i--)
            //    Console.WriteLine(firstTask[i]);

            //Console.WriteLine("Введите размерность:");
            //if (!int.TryParse(Console.ReadLine(), out int n) || n < 0)
            //    n = 0;

            //int[,] a = new int[n, n];

            //for (int i = 0; i < n; i++) 
            //    for (int k = 0; k < n; k++) 
            //        a[i, k] = i - k > 1 ? 0 : 1;

            //PrintMatrix(a, n, n);


            Console.WriteLine("Введите размерность n*m:");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 0)
                n = 0;
            if (!int.TryParse(Console.ReadLine(), out int m) || m < 0)
                m = 0;

            int[,] a = new int[n, m];
            
            // for (var i = 0; i < n; i++)
            //     for (var k = 0; k < n; k++)
            //         a[i, k] = 0;

            FillMatrix(a, 0, n, m, 0);

            PrintMatrix(a, n, m);
        }
    }
}
