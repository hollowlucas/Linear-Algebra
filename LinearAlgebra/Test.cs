using System;

namespace LinearAlgebra
{
    public class Test
    {
        static void Main(string[] args)
        {
            var A = new Matrix(new double[,]
            {
                {6, 1, 1},
                {4, -2, 5},
                {2, 8, 7}
            });
            Console.WriteLine(A);
            Console.WriteLine(A.Determinant);
        }
    }
}