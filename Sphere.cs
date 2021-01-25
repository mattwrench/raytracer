using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class Sphere : Hittable
    {
        public Vector3 Center;
        public double Radius;

        public Sphere()
        {
            Center = new Vector3();
            Radius = 0;
        }

        public Sphere(Vector3 center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Hit(Ray r, double tMin, double tMax, HitRecord record)
        {
            Vector3 oc = r.Origin - Center;
            double a = r.Direction.LengthSquared();
            double halfB = oc.Dot(r.Direction);
            double c = oc.LengthSquared() - Radius * Radius;

            double discriminant = halfB * halfB - a * c;
            if (discriminant < 0)
                return false;
            double sqrtDiscriminant = Math.Sqrt(discriminant);

            // Find the nearest root that lies in the acceptable range
            double root = (-halfB - sqrtDiscriminant) / a;
            if (root < tMin || tMax < root)
            {
                root = (-halfB + sqrtDiscriminant) / a;
                if (root < tMin || tMax < root)
                    return false;
            }

            record.T = root;
            record.Point = r.At(record.T);
            record.Normal = (record.Point - Center) / Radius;
            return true;
        }
    }
}
