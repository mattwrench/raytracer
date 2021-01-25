using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    interface Hittable
    {
        public abstract bool Hit(Ray r, double tMin, double tMax, HitRecord record);
    }
}
