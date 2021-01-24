using System;
using System.IO;

namespace RayTracer
{
    // A C# implementation of the basic ray tracer from Peter Shirley's Ray Tracing in One Weekend
    class Program
    {
        const string Filename = "image.ppm";
        const int ImageWidth = 256;
        const int ImageHeight = 256;

        static void Main(string[] args)
        {
            // Output .ppm header
            StreamWriter writer = new StreamWriter(Filename, false, System.Text.Encoding.ASCII);
            writer.WriteLine("P3"); // ASCII encoding
            writer.WriteLine("{0} {1}", ImageWidth, ImageHeight); // Image resolution
            writer.WriteLine("255"); // Max value of color components

            for (int j = ImageHeight - 1; j >= 0; --j)
            {
                // Progress indicator
                Console.Write("\rScanlines remaining: {0}  ", j); // Extra spaces at end help clear console line
                for (int i = 0; i < ImageWidth; ++i)
                {
                    double r = ((double)i) / (ImageWidth - 1);
                    double g = ((double)j) / (ImageHeight - 1);
                    double b = 0.25;

                    int ir = (int)(255.999 * r);
                    int ig = (int)(255.999 * g);
                    int ib = (int)(255.999 * b);

                    writer.WriteLine("{0} {1} {2}", ir, ig, ib);
                }
            }

            Console.WriteLine("\nDone.");
            writer.Close();
        }
    }
}
