using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;

namespace Rectangles
{
    public static class RectanglesTask
    {
        // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            var fullWidth = r1.Width + r2.Width;
            var fullHeight = r1.Height + r2.Height;
            var checkTop = Math.Abs(r1.Left - r2.Right) <= fullWidth;
            var checkBottom = Math.Abs(r1.Right - r2.Left) <= fullWidth;
            var checkLeft = Math.Abs(r1.Top - r2.Bottom) <= fullHeight;
            var checkRight = Math.Abs(r1.Bottom - r2.Top) <= fullHeight;
            return checkBottom && checkTop && checkLeft && checkRight;
        }

        // Площадь пересечения прямоугольников
        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            var left = Math.Max(r1.Left, r2.Left);
            var top = Math.Max(r1.Top, r2.Top);
            var right = Math.Min(r1.Right, r2.Right);
            var bottom = Math.Min(r1.Bottom, r2.Bottom);
            var width = right - left;
            var height = bottom - top;
            if (width < 0 || height < 0) return 0;
            return width * height;
        }

        // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
        // Иначе вернуть -1
        // Если прямоугольники совпадают, можно вернуть номер любого из них.
        public static bool CheckInner(Rectangle r2, Rectangle r1)
        {
            return r2.Left <= r1.Left && r2.Right >= r1.Right && r2.Top <= r1.Top && r2.Bottom >= r1.Bottom;
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (CheckInner(r1, r2)) return 1;
            if (CheckInner(r2, r1)) return 0;
            return -1;
        }
    }
}