using System;
using System.Diagnostics;

namespace LinearAlgebra
{
    public class Test
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();
            var A = new Matrix(new double[,] {
                {1, 3, 6, -55, 1, 22, 8},
                {-10, 3, 8, 24, 128, 529, 33},
                {6, 1, 3, 9, 6, 8, -3},
                {-3, -76, -2, 6, 1, 5, 2},
                {-51, 12, 3, 7, 3, 1, 7},
                {8, 4, 7, 3, 1, 6, -4},
                {1, 3, -2, -6, 23, 6, 1}
            });
            Console.WriteLine(A.ToArrayString());
            Console.WriteLine(A.Determinant);

            timer.Stop();
            Console.WriteLine(timer.Elapsed);
        }
    }
}