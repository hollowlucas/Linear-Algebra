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

            var a = new Vector(new double[] {6, -4, 7});
            var b = new Vector(new double[] {-3, -9, 8});
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(a.Cross(b).Normalized);

            timer.Stop();
            Console.WriteLine(timer.Elapsed);
        }
    }
}