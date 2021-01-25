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
    }
}
