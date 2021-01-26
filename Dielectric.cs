using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class Dielectric : Material
    {
        public double RefractionIndex;

        public Dielectric(double refractionIndex)
        {
            RefractionIndex = refractionIndex;
        }

        public bool Scatter(Ray rIn, HitRecord record, Vector3 attenuation, Ray scattered, Random rand)
        {
            attenuation.Set(new Vector3(1.0, 1.0, 1.0));
            double refractionRatio = record.FrontFace ? (1.0 / RefractionIndex) : RefractionIndex;

            Vector3 unitDirection = rIn.Direction.Normalize();
            Vector3 refracted = Vector3.Refract(unitDirection, record.Normal, refractionRatio);

            scattered.Set(new Ray(record.Point, refracted));
            return true;
        }
    }
}
