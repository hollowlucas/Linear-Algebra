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
            var A = new Matrix(2, 2, MatrixUtil.Increment);
            Console.WriteLine(A.Inverse);

            timer.Stop();
            Console.WriteLine(timer.Elapsed);
        }
    }
}