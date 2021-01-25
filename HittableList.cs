using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class HittableList : Hittable
    {
        public List<Hittable> HittableObjects;

        public HittableList()
        {
            HittableObjects = new List<Hittable>();
        }

        public void Add(Hittable hittable)
        {
            HittableObjects.Add(hittable);
        }

        // Checks for collisions with all hittable objects
        public bool Hit(Ray r, double tMin, double tMax, HitRecord record)
        {
            HitRecord tempRecord = new HitRecord();
            bool hitAnything = false;
            double closestSoFar = tMax;

            foreach (Hittable hittable in HittableObjects)
            {
                if (hittable.Hit(r, tMin, closestSoFar, tempRecord))
                {
                    hitAnything = true;
                    closestSoFar = tempRecord.T;
                    record = tempRecord;
                }
            }

            return hitAnything;
        }
    }
}
