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
                Ray scattered = new Ray();
                Vector3 attenuation = new Vector3();
                if (record.Material.Scatter(r, record, attenuation, scattered, rand))
                    return attenuation * rayColor(scattered, world, depth - 1);
                return new Vector3(0, 0, 0); // Return black
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
            double R = Math.Cos(Math.PI / 4);
            HittableList world = new HittableList();

            Lambertian materialGround = new Lambertian(new Vector3(0.8, 0.8, 0.0));
            Lambertian materialCenter = new Lambertian(new Vector3(0.1, 0.2, 0.5));
            Dielectric materialLeft = new Dielectric(1.5);
            Metal materialRight = new Metal(new Vector3(0.8, 0.6, 0.2), 0.0);

            world.Add(new Sphere(new Vector3(0.0, -100.5, -1.0), 100.0, materialGround));
            world.Add(new Sphere(new Vector3(0.0, 0.0, -1.0), 0.5, materialCenter));
            world.Add(new Sphere(new Vector3(-1.0, 0.0, -1.0), 0.5, materialLeft));
            world.Add(new Sphere(new Vector3(-1.0, 0.0, -1.0), -0.4, materialLeft));
            world.Add(new Sphere(new Vector3(1.0, 0.0, -1.0), 0.5, materialRight));

            // Camera
            Camera cam = new Camera(new Vector3(-2, 2, 1), new Vector3(0, 0, -1), new Vector3(0, 1, 0), 20, aspectRatio);

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
                    if (pixelColor.X == 0 && pixelColor.Y == 0 && pixelColor.Z == 0)
                        ;
                    writer.WriteLine(pixelColor.WriteColor(pixelColor, samplesPerPixel));
                }
            }

            Console.WriteLine("\nDone.");
            writer.Close();
        }
    }
}
