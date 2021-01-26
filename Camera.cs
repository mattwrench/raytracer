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

        public Camera(Vector3 lookFrom, Vector3 lookAt, Vector3 vUp, double vFOV, double aspectRatio)
        {
            double theta = Utilities.DegreesToRadians(vFOV);
            double h = Math.Tan(theta / 2);
            double viewportHeight = 2.0 * h;
            double viewportWidth = aspectRatio * viewportHeight;

            Vector3 w = (lookFrom - lookAt).Normalize();
            Vector3 u = vUp.Cross(w).Normalize();
            Vector3 v = w.Cross(u);

            origin = lookFrom;
            horizontal = viewportWidth * u;
            vertical = viewportHeight * v;
            lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - w;
        }

        public Ray GetRay(double s, double t)
        {
            return new Ray(origin, lowerLeftCorner + s * horizontal + t * vertical - origin);
        }
    }
}
