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
            set
            {
                Components[0] = value;
            }
        }

        public double Y
        {
            get
            {
                return Components[1];
            }
            set
            {
                Components[1] = value;
            }
        }

        public double Z
        {
            get
            {
                return Components[2];
            }
            set
            {
                Components[2] = value;
            }
        }

        public Vector3 Set(Vector3 v)
        {
            Components[0] = v.X;
            Components[1] = v.Y;
            Components[2] = v.Z;
            return this; // Returns updated vector for further chaining
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
            if (LengthSquared() > 0) // Prevents division by 0
                return this / Length();
            return this;
        }

        public override string ToString()
        {
            return String.Concat(X, ' ', Y, ' ', Z);
        }

        public string WriteColor(Vector3 pixelColor, int samplesPerPixel)
        {
            double r = pixelColor.X;
            double g = pixelColor.Y;
            double b = pixelColor.Z;

            // Divide the color by the number of samples and gamma-correct for gamma = 2.0
            double scale = 1.0 / samplesPerPixel;
            r = Math.Sqrt(scale * r);
            g = Math.Sqrt(scale * g);
            b = Math.Sqrt(scale * b);

            // Translate values to be between [0, 255]
            r = 256 * Utilities.Clamp(r, 0.0, 0.999);
            g = 256 * Utilities.Clamp(g, 0.0, 0.999);
            b = 256 * Utilities.Clamp(b, 0.0, 0.999);

            return String.Concat(r, ' ', g, ' ', b);
        }

        public bool NearZero()
        {
            // Returns true if vector is close to 0 in all directions
            double s = 1e-8;
            return (Math.Abs(Components[0]) < s) &&
                (Math.Abs(Components[1]) < s) &&
                (Math.Abs(Components[2]) < s);
        }

        // Static functions
        public static Vector3 RandomVector(Random rand)
        {
            return new Vector3(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
        }

        public static Vector3 RandomVector(Random rand, double min, double max)
        {
            return new Vector3(min + (max - min) * rand.NextDouble(),
                               min + (max - min) * rand.NextDouble(),
                               min + (max - min) * rand.NextDouble());
        }

        public static Vector3 RandomInUnitSphere(Random rand)
        {
            while (true)
            {
                Vector3 p = RandomVector(rand, -1, 1);
                if (p.LengthSquared() >= 1)
                    continue;
                return p;
            }
        }

        public static Vector3 RandomUnitVector(Random rand)
        {
            return RandomInUnitSphere(rand).Normalize();
        }

        public static Vector3 RandomInUnitDisk(Random rand)
        {
            while (true)
            {
                Vector3 p = new Vector3(Utilities.RandomDouble(rand, -1, 1), Utilities.RandomDouble(rand, -1, 1), 0);
                if (p.LengthSquared() >= 1)
                    return p;
            }
        }

        public static Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * v.Dot(n) * n;
        }

        public static Vector3 Refract(Vector3 uv, Vector3 n, double etaiOverEtat)
        {
            double cosTheta = Math.Min(n.Dot(-uv), 1.0);
            Vector3 rOutPerp = etaiOverEtat * (uv + cosTheta * n);
            Vector3 rOutParallel = -Math.Sqrt(Math.Abs(1.0 - rOutPerp.LengthSquared())) * n;
            return rOutPerp + rOutParallel;
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

        // Unary inversion
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
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
