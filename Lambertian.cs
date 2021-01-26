using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    // Class for diffuse/matte materials
    class Lambertian : Material
    {
        public Vector3 Albedo;

        public Lambertian(Vector3 a)
        {
            Albedo = a;
        }

        public bool Scatter(Ray rIn, HitRecord record, Vector3 attenuation, Ray scattered, Random rand)
        {
            Vector3 scatterDirection = record.Normal + Vector3.RandomUnitVector(rand);

            // Catch degenerate scatter direction
            if (scatterDirection.NearZero())
                scatterDirection = record.Normal;

            scattered.Set(new Ray(record.Point, scatterDirection));
            attenuation.Set(Albedo);
            return true;
        }
    }
}
