using System;
using System.Diagnostics;

namespace LinearAlgebra
{
    class Vector
    {
        /// <summary>
        /// The amount of components the vector has.
        /// </summary>
        public int Dimensions { get; private set; }
        
        /// <summary>
        /// Indexer for getting a component of the vector.
        /// </summary>
        /// <param name="index"></param>
        public double this[int index]
        {
            get => components[index - 1];
            set => components[index - 1] = value;
        }
        
        /// <summary>
        /// Internal array of components.
        /// </summary>
        private double[] components;

        /// <summary>
        /// Creates a vector from an 1D array.
        /// </summary>
        /// <param name="components"></param>
        public Vector(double[] components)
        {
            Dimensions = components.Length;
            this.components = components;
        }
        
        /// <summary>
        /// Creates a copy of a vector.
        /// </summary>
        /// <param name="source"></param>
        public Vector(Vector source)
        {
            var newComps = new double[source.Dimensions];
            Array.Copy(source.components, 0, newComps, 0, source.Dimensions);
            Dimensions = source.Dimensions;
            components = newComps;
        }
        
        /// <summary>
        /// Creates a zero vector
        /// </summary>
        /// <param name="dimensions"></param>
        public Vector(int dimensions)
        {
            Dimensions = components.Length;
            components = new double[Dimensions];
        }

        /// <summary>
        /// Converts a vector to a string [ a, b, ... ]
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = "[ ";
            for (int i = 1; i <= Dimensions; i++)
            {
                str += $"{this[i]} ";
            }

            str += "]";
            
            return str;
        }
        
        /// <summary>
        /// Multiplies each component of the vector by a scalar.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Vector operator *(double scale, Vector a)
        {
            var b = new Vector(a);
            for (int i = 1; i <= b.Dimensions; i++)
            {
                b[i] = scale * b[i];
            }

            return b;
        }

        /// <summary>
        /// Multiplies each component of the vector by a scalar.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector operator *(Vector a, double scale)
        {
            return scale * a;
        }
        
        /// <summary>
        /// Adds vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector operator +(Vector a, Vector b)
        {
            Debug.Assert(a.Dimensions == b.Dimensions, "Vectors are not equal in dimensions.");
            var vec = new Vector(a.Dimensions);
            for (int i = 1; i <= a.Dimensions; i++)
            {
                vec[i] = a[i] + b[i];
            }

            return vec;
        }
        
        /// <summary>
        /// Subtracts b from a
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector operator -(Vector a, Vector b)
        {
            return a + -1 * b;
        }

        /// <summary>
        /// Returns the dot product of the vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public double Dot(Vector a)
        {
            Debug.Assert(Dimensions == a.Dimensions, "Vectors are not equal in dimensions.");
            var sum = 0.0;
            for (int i = 1; i <= Dimensions; i++)
            {
                sum += this[i] * a[i];
            }

            return sum;
        }
    }
}