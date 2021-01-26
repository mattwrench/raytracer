using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    interface Material
    {
        public abstract bool Scatter(Ray rIn, HitRecord record, Vector3 attenuation, Ray scattered, Random rand);
    }
}
