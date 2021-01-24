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

        Vector3 At(double t)
        {
            return Origin.Add(Direction.Multiply(t));
        }
    }
}
