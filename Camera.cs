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
        private Vector3 u, v, w;
        private double lensRadius;

        public Camera(
            Vector3 lookFrom, 
            Vector3 lookAt, 
            Vector3 vUp, 
            double vFOV, 
            double aspectRatio,
            double aperture,
            double focusDist)
        {
            double theta = Utilities.DegreesToRadians(vFOV);
            double h = Math.Tan(theta / 2);
            double viewportHeight = 2.0 * h;
            double viewportWidth = aspectRatio * viewportHeight;

            w = (lookFrom - lookAt).Normalize();
            u = vUp.Cross(w).Normalize();
            v = w.Cross(u);

            origin = lookFrom;
            horizontal = focusDist * viewportWidth * u;
            vertical = focusDist * viewportHeight * v;
            lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - focusDist * w;

            lensRadius = aperture / 2;
        }

        public Ray GetRay(double s, double t, Random rand)
        {
            Vector3 rd = lensRadius * Vector3.RandomInUnitDisk(rand);
            Vector3 offset = u * rd.X + v * rd.Y;

            return new Ray(origin + offset, 
                lowerLeftCorner + s * horizontal + t * vertical - origin - offset);
        }
    }
}
