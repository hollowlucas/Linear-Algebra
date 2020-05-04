using System;

namespace LinearAlgebra
{
    public class Test
    {
        static void Main(string[] args)
        {
            var A = new Matrix(3, 3, MatrixUtil.Increment);
            Console.WriteLine(A);
            Console.WriteLine(A.Determinant);
            Console.WriteLine(A * A);
        }
    }
}