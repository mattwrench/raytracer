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
    }
}
