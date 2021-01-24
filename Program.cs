using System;
using System.IO;

namespace RayTracer
{
    // A C# implementation of the basic ray tracer from Peter Shirley's Ray Tracing in One Weekend
    class Program
    {
        const string Filename = "image.ppm";

        private static Vector3 rayColor(Ray r)
        {
            // Render a blue-to-white gradient background
            Vector3 unitDirection = r.Direction.Normalize();
            double t = 0.5 * (unitDirection.Y + 1);
            Vector3 white = (1.0 - t) * new Vector3(1.0, 1.0, 1.0);
            Vector3 blue = t * new Vector3(0.5, 0.7, 1.0);
            return white + blue;
        }

        public static void Main(string[] args)
        {
            // Image
            const double aspectRatio = 16.0 / 9.0;
            const int imageWidth = 400;
            const int imageHeight = (int)(imageWidth / aspectRatio);

            // Camera
            double viewportHeight = 2.0;
            double viewportWidth = aspectRatio * viewportHeight;
            double focalLength = 1.0;

            Vector3 origin = new Vector3(0, 0, 0);
            Vector3 horizontal = new Vector3(viewportWidth, 0, 0);
            Vector3 vertical = new Vector3(0, viewportHeight, 0);
            Vector3 lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focalLength);

            // Output .ppm header
            StreamWriter writer = new StreamWriter(Filename, false, System.Text.Encoding.ASCII);
            writer.WriteLine("P3"); // ASCII encoding
            writer.WriteLine("{0} {1}", imageWidth, imageHeight); // Image resolution
            writer.WriteLine("255"); // Max value of color components

            for (int j = imageHeight - 1; j >= 0; --j)
            {
                // Progress indicator
                Console.Write("\rScanlines remaining: {0}  ", j); // Extra spaces at end help clear console line
                for (int i = 0; i < imageWidth; ++i)
                {
                    double u = ((double)i) / (imageWidth - 1);
                    double v = ((double)j) / (imageHeight - 1);
                    Vector3 dir = lowerLeftCorner + u * horizontal + v * vertical - origin;
                    Ray r = new Ray(origin, dir);
                    Vector3 pixelColor = rayColor(r);
                    writer.WriteLine(pixelColor.WriteColor());
                }
            }

            Console.WriteLine("\nDone.");
            writer.Close();
        }
    }
}
