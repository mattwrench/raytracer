using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class Camera
    {
        private Vector3 origin;
        private Vector3 lowerLeftCorner;
        private Vector3 horizontal, vertical;

        public Camera()
        {
            double aspectRatio = 16.0 / 9.0;
            double viewportHeight = 2.0;
            double viewportWidth = aspectRatio * viewportHeight;
            double focalLength = 1.0;

            origin = new Vector3(0, 0, 0);
            horizontal = new Vector3(viewportWidth, 0.0, 0.0);
            vertical = new Vector3(0.0, viewportHeight, 0.0);
            lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focalLength);
        }

        public Ray GetRay(double u, double v)
        {
            return new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);
        }
    }
}
