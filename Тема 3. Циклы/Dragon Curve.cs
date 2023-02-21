using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        static double sin45 = Math.Sin(Math.PI / 4);
        static double cos45 = Math.Cos(Math.PI / 4);

        static double CalculateX(double x, double y, double angleCos)
        {
            return (x * angleCos - y * sin45) / Math.Sqrt(2);
        }

        static double CalculateY(double x, double y, double angleCos)
        {
            return (x * sin45 + y * angleCos) / Math.Sqrt(2);
        }

        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var x = 1.0;
            var y = 0.0;
            var random = new Random(seed);
            for (var i = 0; i < iterationsCount; i++)
            {
                var number = random.Next(2);
                double changedX, changedY;
                if (number == 0)
                {
                    changedX = CalculateX(x, y, cos45);
                    changedY = CalculateY(x, y, cos45);
                }
                else
                {
                    changedX = CalculateX(x, y, -cos45) + 1;
                    changedY = CalculateY(x, y, -cos45);
                }
                x = changedX;
                y = changedY;
                pixels.SetPixel(x, y);
            }
        }
    }
}