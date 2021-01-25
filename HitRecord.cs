using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class HitRecord
    {
        public Vector3 Point;
        public Vector3 Normal;
        public double T;
        public bool FrontFace;

        public void SetFaceNormal(Ray r, Vector3 outwardNormal)
        {
            FrontFace = r.Direction.Dot(outwardNormal) < 0;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
        }

        // Set this HitRecord equal to another given record
        public void Set(HitRecord r)
        {
            Point = r.Point;
            Normal = r.Normal;
            T = r.T;
            FrontFace = r.FrontFace;
        }
    }
}
