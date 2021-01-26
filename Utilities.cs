using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    static class Utilities
    {
        public static double Clamp(double x, double min, double max)
        {
            if (x < min)
                return min;
            if (x > max)
                return max;
            return x;
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees / 180 * Math.PI;
        }

        // Returns double between min and max
        public static double RandomDouble(Random rand, double min, double max)
        {
            return min + (max - min) * rand.NextDouble();
        }
    }
}
