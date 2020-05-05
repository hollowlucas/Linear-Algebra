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

            var a = new Vector(new double[] {1, 2, 0});
            var b = new Vector(new double[] {2, 1, 0});
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(a.Cross(b));

            timer.Stop();
            Console.WriteLine(timer.Elapsed);
        }
    }
}