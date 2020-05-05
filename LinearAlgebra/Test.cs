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

            var a = new Matrix(new double[,]
            {
                {-3, 0, 3, 2},
                {1, 7, -1, 9}
            });
            var b = new Vector(new double[]
            {
                1, 1, 0, 0
            });
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(a * b);

            timer.Stop();
            Console.WriteLine(timer.Elapsed);
        }
    }
}