using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class Ray
    {
        public Vector3 Origin;
        public Vector3 Direction;

        public Ray()
        {
            Origin = new Vector3();
            Direction = new Vector3();
        }

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 At(double t)
        {
            return Origin.Add(Direction.Multiply(t));
        }

        public Ray Set(Ray r)
        {
            Origin.Set(r.Origin);
            Direction.Set(r.Direction);
            return this; // Returns ray for further chaining
        }
    }
}
