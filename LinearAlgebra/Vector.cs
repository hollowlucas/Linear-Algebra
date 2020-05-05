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
        /// Returns the square magnitude of the vector.
        /// </summary>
        public double SqrMagnitude => this.Dot(this);

        /// <summary>
        /// Returns the magnitude of the vector.
        /// </summary>
        public double Magnitude => Math.Sqrt(SqrMagnitude);

        /// <summary>
        /// Returns a new vector which is normalized
        /// </summary>
        public Vector Normalized
        {
            get
            {
                var ret = new Vector(this);
                var mag = Magnitude;
                for (int i = 1; i <= ret.Dimensions; i++)
                {
                    ret[i] /= mag;
                }

                return ret;
            }
        }

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
            components = new double[dimensions];
            Dimensions = dimensions;
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
        
        /// <summary>
        /// Returns the angle between the vectors in radians.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public double Angle(Vector a)
        {
            return Math.Acos(Dot(a) / (Magnitude * a.Magnitude));
        }
        
        /// <summary>
        /// Returns a vector that is orthogonal to both vectors.
        /// Only works with 3D vectors.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Vector Cross(Vector b)
        {
            Debug.Assert(Dimensions == b.Dimensions && Dimensions == 3, "Both vectors must be 3-dimensional");
            var cross = new Vector(Dimensions);
            cross[1] = this[2] * b[3] - this[3] * b[2];
            cross[2] = this[3] * b[1] - this[1] * b[3];
            cross[3] = this[1] * b[2] - this[2] * b[1];
            return cross;
        }
    }
}