using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    // Class for reflective objects
    class Metal : Material
    {
        public Vector3 Albedo;
        public double Fuzz;

        public Metal(Vector3 a, double f)
        {
            Albedo = a;
            Fuzz = f;
        }

        public bool Scatter(Ray rIn, HitRecord record, Vector3 attenuation, Ray scattered, Random rand)
        {
            Vector3 reflected = Vector3.Reflect(rIn.Direction.Normalize(), record.Normal);

            // Save scattered & attenuation values
            scattered.Set(new Ray(record.Point, reflected + Fuzz * Vector3.RandomInUnitSphere(rand)));
            attenuation.Set(Albedo);

            return scattered.Direction.Dot(record.Normal) > 0;
        }
    }
}
