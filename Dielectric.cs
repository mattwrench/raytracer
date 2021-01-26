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
            double cosTheta = Math.Min(record.Normal.Dot(-unitDirection), 1.0);
            double sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);

            bool cannotRefract = refractionRatio * sinTheta > 1.0;
            Vector3 direction;

            if (cannotRefract)
                direction = Vector3.Reflect(unitDirection, record.Normal);
            else
                direction = Vector3.Refract(unitDirection, record.Normal, refractionRatio);

            scattered.Set(new Ray(record.Point, direction));
            return true;
        }
    }
}
