using System;
using System.Threading;
using System.Xml.Schema;

namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double DistanceBetweenPoints(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            var ab = DistanceBetweenPoints(ax, bx, ay, by);
            var bc = DistanceBetweenPoints(bx, x, by, y);
            var ac = DistanceBetweenPoints(ax, x, ay, y);
            var perimeterHalf = (ac + ab + bc) / 2;
            if (ac * ac >= ab * ab + bc * bc || bc * bc >= ab * ab + ac * ac)
                return Math.Min(ac, bc);
            return 2 * (Math.Sqrt(perimeterHalf * (perimeterHalf - ab) * 
                                  (perimeterHalf - bc) * (perimeterHalf - ac))) / ab;
        }
    }
}