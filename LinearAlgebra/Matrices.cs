using System;
using System.Diagnostics;

namespace LinearAlgebra
{
    public delegate double MatrixInitializer(int row, int column);
    public delegate double MatrixInitializer2(int row, int column, int rows, int columns);

    
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
    
    class Matrix
    {        
        /// <summary>
        /// Amount of rows (height) in the matrix.
        /// </summary>
        public int Rows { get; private set; }
        
        /// <summary>
        /// Amount of columns (width) in the matrix.
        /// </summary>
        public int Columns { get; private set; }
        
        /// <summary>
        /// Indexer for getting an element in this matrix.
        /// </summary>
        /// <param name="row">Vertical position</param>
        /// <param name="column">Horizontal position</param>
        public double this[int row, int column]
        {
            get => elements[row - 1, column - 1];
            set => elements[row - 1, column - 1] = value;
        }

        /// <summary>
        /// The amount of rows in the matrix equal the amount of columns in the matrix
        /// </summary>
        public bool IsSquare => Rows == Columns;

        /// <summary>
        /// The internal elements of the matrix.
        /// </summary>
        private double[,] elements;

        /// <summary>
        /// Creates a matrix from a 2D array.
        /// </summary>
        /// <param name="elements"></param>
        public Matrix(double[,] elements)
        {
            this.elements = elements;
            Rows = elements.GetLength(0);
            Columns = elements.GetLength(1);
        }
        
        /// <summary>
        /// Creates an empty MxN matrix.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Matrix(int rows, int columns)
        {
            elements = CreateElements(rows, columns);
            Rows = elements.GetLength(0);
            Columns = elements.GetLength(1);
        }
        
        /// <summary>
        /// Creates a matrix using an initializer delegate (used to set each value).
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="initializer"></param>
        public Matrix(int rows, int columns, MatrixInitializer initializer)
        {
            elements = CreateElements(rows, columns);
            Rows = elements.GetLength(0);
            Columns = elements.GetLength(1);
            SetValues(initializer);
        }
        
        /// <summary>
        /// Creates a matrix using an initializer delegate (used to set each value).
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="initializer"></param>
        public Matrix(int rows, int columns, MatrixInitializer2 initializer)
        {
            elements = CreateElements(rows, columns);
            Rows = elements.GetLength(0);
            Columns = elements.GetLength(1);
            SetValues(initializer);
        }

        /// <summary>
        /// Sets the values of a matrix using an initializer.
        /// </summary>
        /// <param name="initializer"></param>
        public void SetValues(MatrixInitializer initializer)
        {
            for (int r = 1; r <= Rows; r++)
            {
                for (int c = 1; c <= Columns; c++)
                {
                    this[r, c] = initializer(r, c);
                }
            }
        }
        
        /// <summary>
        /// Sets the values of a matrix using an initializer.
        /// </summary>
        /// <param name="initializer"></param>
        public void SetValues(MatrixInitializer2 initializer)
        {
            for (int r = 1; r <= Rows; r++)
            {
                for (int c = 1; c <= Columns; c++)
                {
                    this[r, c] = initializer(r, c, Rows, Columns);
                }
            }
        }
        
        /// <summary>
        /// Creates a copy of a matrix.
        /// </summary>
        /// <param name="A"></param>
        public Matrix(Matrix A)
        {
            Rows = A.Rows;
            Columns = A.Columns;
            elements = CreateElements(A.Rows, A.Columns);
            
            for (int r = 1; r <= Rows; r++)
            {
                for (int c = 1; c <= Columns; c++)
                {
                    this[r, c] = A[r, c];
                }
            }
        }

        /// <summary>
        /// Converts the matrix to a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = $"{Rows}x{Columns} Matrix:\n";
            for (int y = 1; y <= Rows; y++)
            {
                var row = "|\t";
                for (int x = 1; x <= Columns; x++)
                {
                    row += $"{this[y, x]} \t";
                }
                row += "|";
                if (y < Rows) row += "\n";
                
                str += row;
            }
            
            return str;
        }

        /// <summary>
        /// Returns if the dimensions of this matrix and another matrix are the same.
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public bool SameDimensions(Matrix A)
        {
            return Rows == A.Rows && Columns == A.Columns;
        }
        
        /// <summary>
        /// Creates an array for the elements of a matrix.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static double[,] CreateElements(int rows, int columns)
        {
            var elements = new double[rows, columns];
            return elements;
        }

        /// <summary>
        /// Get a row vector (horizontal) from the matrix.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Vector GetRowVector(int row)
        {
            var rowElements = new double[Columns];
            for (int c = 1; c <= Columns; c++)
            {
                rowElements[c - 1] = this[row, c];
            }
            return new Vector(rowElements);
        }
        
        /// <summary>
        /// Get a column vector (vertical) from the matrix.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Vector GetColumnVector(int column)
        {
            var columnElements = new double[Rows];
            for (int r = 1; r <= Rows; r++)
            {
                columnElements[r - 1] = this[r, column];
            }
            return new Vector(columnElements);
        }

        /// <summary>
        /// Adds each element in A to the corresponding element in B.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            Debug.Assert(A.SameDimensions(B), "Matrices aren't the same dimensions");
            var ret = new Matrix(A.Rows, A.Columns);
            for (int r = 1; r <= ret.Rows; r++)
            {
                for (int c = 1; c <= ret.Columns; c++)
                {
                    ret[r, c] = A[r, c] + B[r, c];
                }
            }
            return ret;
        }
        
        /// <summary>
        /// Subtracts each element in B from the corresponding element in A.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            return A + -1 * B;
        }
        
        /// <summary>
        /// Multiplies each element in the matrix by a scalar.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix operator *(double scale, Matrix A)
        {
            var ret = new Matrix(A.Rows, A.Columns);
            for (int r = 1; r <= ret.Rows; r++)
            {
                for (int c = 1; c <= ret.Columns; c++)
                {
                    ret[r, c] = scale * A[r, c];
                }
            }
            return ret;
        }
        
        /// <summary>
        /// Multiplies each element in the matrix by a scalar.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix A, double scale)
        {
            return scale * A;
        }
        
        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// The rows of the first matrix need to be the same as the columns of the second matrix.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            Debug.Assert(A.Rows == B.Columns, "Rows of first matrix must equal columns of second matrix.");
            var ret = new Matrix(A.Rows, B.Columns);
            for (int r = 1; r <= A.Rows; r++)
            {
                for (int c = 1; c <= B.Columns; c++)
                {
                    ret[r, c] = A.GetRowVector(r).Dot(B.GetColumnVector(c));
                }
            }
            return ret;
        }
        
        /// <summary>
        /// Returns the determinant of the matrix.
        /// Needs to be a square matrix.
        /// </summary>
        public double Determinant
        {
            get
            {
                Debug.Assert(IsSquare, "Must be a square matrix to calculate the determinant");
                if (Rows == 1) return this[1, 1];
                if (Rows == 2) return this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1];
                
                var sum = 0.0;
                var mult = 1;
                for (int c = 1; c <= Columns; c++)
                {
                    sum += mult * GetDeterminant(c);
                    mult *= -1;
                }
                
                return sum;
            }
        }

        /// <summary>
        /// Gets the sub determinant of this matrix, ignoring the top row and a column, (Row - 1) by (Column - 1)
        /// </summary>
        /// <param name="ignoredColumn"></param>
        /// <returns></returns>
        private double GetDeterminant(int ignoredColumn)
        {
            var subMatrix = new Matrix(Rows - 1, Columns - 1);
            for (int r = 2; r <= Rows; r++)
            {
                for (int c = 1; c <= Columns; c++)
                {
                    if (c == ignoredColumn) continue;
                    subMatrix[r - 1, c > ignoredColumn ? c - 1 : c] = this[r, c];
                    // Ignores the skipped rows and columns and sets the values of the sub matrix
                    // XXX XXX XXX
                    // XAA AXA AAX
                    // XAA AXA AAX
                }
            }
            //Console.WriteLine($"{this[1, ignoredColumn]} + {subMatrix}");
            return this[1, ignoredColumn] * subMatrix.Determinant;
        }
    }

    static class MatrixUtil
    {
        /// <summary>
        /// Generates a checkerboard of 1s and -1s for calculating a matrix of cofactors
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static double Checkerboard(int row, int column, int rows, int columns)
        {
            var val = ((row * columns + rows) + column) % 2;
            if (val == 1) return 1;
            return -1;
        }
        
        /// <summary>
        /// Generates an incrementing matrix (eg. 1,2,3,4)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static double Increment(int row, int column, int rows, int columns)
        {
            return (row - 1) * columns + column;
        }
    }
}