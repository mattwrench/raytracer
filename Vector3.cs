﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class Vector3
    {
        private const int NumDimensions = 3;

        public double[] Components;

        public Vector3()
        {
            Components = new double[] { 0, 0, 0};
        }

        public Vector3(double e0, double e1, double e2)
        {
            Components = new double[] { e0, e1, e2 };
        }

        public Vector3(Vector3 v)
        {
            Components = new double[] { v.X, v.Y, v.Z };
        }

        public double X
        {
            get
            {
                return Components[0];
            }
        }

        public double Y
        {
            get
            {
                return Components[1];
            }
        }

        public double Z
        {
            get
            {
                return Components[2];
            }
        }

        public Vector3 Add(Vector3 v)
        {
            return new Vector3(X + v.X, Y + v.Y, Z + v.Z);
        }

        public Vector3 Add(double x, double y, double z)
        {
            return new Vector3(X + x, Y + y, Z + z);
        }

        public Vector3 Subtract(Vector3 v)
        {
            return Add(-v.X, -v.Y, -v.Z);
        }

        public Vector3 Subtract(double x, double y, double z)
        {
            return Add(-x, -y, -z);
        }

        public Vector3 Multiply(Vector3 v)
        {
            return Multiply(v.X, v.Y, v.Z);
        }

        public Vector3 Multiply (double x, double y, double z)
        {
            return new Vector3(X * x, Y * y, Z * z);
        }
        
        public Vector3 Multiply(double t)
        {
            return new Vector3(X * t, Y * t, Z * t);
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public double LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Dot(Vector3 v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        public Vector3 Cross(Vector3 v)
        {
            return new Vector3(Y * v.Z - Z * v.Y,
                               Z * v.X - X * v.Z,
                               X * v.Y - Y * v.X);
        }

        public Vector3 Normalize()
        {
            return this / Length();
        }

        public override string ToString()
        {
            return String.Concat(X, ' ', Y, ' ', Z);
        }

        public string WriteColor()
        {
            int ir = (int)(255.999 * X);
            int ig = (int)(255.999 * Y);
            int ib = (int)(255.999 * Z);
            return String.Concat(ir, ' ', ig, ' ', ib);
        }

        // Operator overloads
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3 operator *(double a, Vector3 b)
        {
            return b * a;
        }

        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static Vector3 operator /(Vector3 a, double b)
        {
            return a * (1 / b);
        }

    }
}
