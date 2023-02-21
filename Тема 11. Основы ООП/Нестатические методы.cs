using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public Vector()
        {
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
        
        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(vector, this);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public static class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector { X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y };
        }

        public static double GetLength(Segment segment)
        {
            return Math.Sqrt(
                (segment.End.X - segment.Begin.X) * (segment.End.X - segment.Begin.X)
                + (segment.End.Y - segment.Begin.Y) * (segment.End.Y - segment.Begin.Y));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var vectorSegment = new Vector { X = segment.End.X - segment.Begin.X, Y = segment.End.Y - segment.Begin.Y };
            var vectorBetweenBeginAndPoint = new Vector
                { X = segment.Begin.X - vector.X, Y = segment.Begin.Y - vector.Y };
            var vectorBetweenPointAndEnd = new Vector
                { X = segment.End.X - vector.X, Y = segment.End.Y - vector.Y };
            return Math.Abs(GetLength(segment) - GetLength(vectorBetweenBeginAndPoint)
                                               - GetLength(vectorBetweenPointAndEnd)) < 1e-5;
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public Segment()
        {
        }

        public Segment(Vector vector1, Vector vector2)
        {
            Begin = vector1;
            End = vector2;
        }

        public double GetLength(Segment segment)
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }
}