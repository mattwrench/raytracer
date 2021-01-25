using System;
using System.IO;

namespace RayTracer
{
    // A C# implementation of the basic ray tracer from Peter Shirley's Ray Tracing in One Weekend
    class Program
    {
        const string Filename = "image.ppm";

        private static Random rand = new Random();

        private static Vector3 rayColor(Ray r, Hittable world, int depth)
        {
            HitRecord record = new HitRecord();

            // Stop gathering light if the ray bounce limit is exceeded
            // Prevents stack overflow due to recursive function in the next section
            if (depth <= 0)
                return new Vector3(0, 0, 0);

            // 0.001 minimum prevents shadow acne
            if (world.Hit(r, 0.001, Double.MaxValue, record))
            {
                Vector3 target = record.Point + record.Normal + Vector3.RandomUnitVector(rand);
                return 0.5 * rayColor(new Ray(record.Point, target - record.Point), world, depth - 1);
            }

            // Render a blue-to-white gradient background if no hit
            Vector3 unitDirection = r.Direction.Normalize();
            double t = 0.5 * (unitDirection.Y + 1);
            Vector3 white = (1.0 - t) * new Vector3(1.0, 1.0, 1.0);
            Vector3 blue = t * new Vector3(0.5, 0.7, 1.0);
            return white + blue;
        }

        private static double hitSphere(Vector3 center, double radius, Ray r)
        {
            // Calculates discriminant from quadraditic equation between intersection of ray and spheres
            // Discriminant > 0 == 2 intersections
            Vector3 oc = r.Origin - center;
            double a = r.Direction.LengthSquared();
            double half_b = oc.Dot(r.Direction);
            double c = oc.LengthSquared() - radius * radius;
            double discriminant = half_b * half_b - a * c;
            if (discriminant < 0)
                return -1.0;
            else
                return (-half_b - Math.Sqrt(discriminant)) / a;
        }

        public static void Main(string[] args)
        {
            // Image
            const double aspectRatio = 16.0 / 9.0;
            const int imageWidth = 400;
            const int imageHeight = (int)(imageWidth / aspectRatio);
            const int samplesPerPixel = 100;
            const int maxDepth = 50;

            // World
            HittableList world = new HittableList();
            world.Add(new Sphere(new Vector3(0, 0, -1), 0.5));
            world.Add(new Sphere(new Vector3(0, -100.5, -1), 100));

            // Camera
            Camera cam = new Camera();

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
                    Vector3 pixelColor = new Vector3(0, 0, 0);
                    for (int s = 0; s < samplesPerPixel; ++s)
                    {
                        // Generate randomized rays from camera viewport
                        double u = (i + rand.NextDouble()) / (imageWidth - 1);
                        double v = (j + rand.NextDouble()) / (imageHeight - 1);
                        Ray r = cam.GetRay(u, v);
                        pixelColor = pixelColor + rayColor(r, world, maxDepth);
                    }
                    writer.WriteLine(pixelColor.WriteColor(pixelColor, samplesPerPixel));
                }
            }

            Console.WriteLine("\nDone.");
            writer.Close();
        }
    }
}
