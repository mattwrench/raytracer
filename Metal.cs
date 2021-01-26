using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    // Class for reflective objects
    class Metal : Material
    {
        public Vector3 Albedo;

        public Metal(Vector3 a)
        {
            Albedo = a;
        }

        public bool Scatter(Ray rIn, HitRecord record, Vector3 attenuation, Ray scattered, Random rand)
        {
            if (rIn.Direction.X == 0 && rIn.Direction.Y == 0 && rIn.Direction.Z == 0)
                ;
            Vector3 reflected = Vector3.Reflect(rIn.Direction.Normalize(), record.Normal);

            // Save scattered & attenuation values
            scattered.Set(new Ray(record.Point, reflected));
            attenuation.Set(Albedo);

            return scattered.Direction.Dot(record.Normal) > 0;
        }
    }
}
