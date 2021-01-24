using System;
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
    }
}
