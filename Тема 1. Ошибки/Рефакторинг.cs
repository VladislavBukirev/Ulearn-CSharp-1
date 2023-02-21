using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Painter
    {
        static float x, y;
        static Graphics graphic;

        public static void Initialize(Graphics newGraphic)
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawLine(Pen pen, double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
	{
		static float bigSide = 0.375f;
		static float smallSide = 0.04f;
        public static void DrawASide(int minimumSide, double turn)
        {
            Painter.DrawLine(Pens.Yellow, minimumSide * bigSide, turn);
            Painter.DrawLine(Pens.Yellow, minimumSide * smallSide * Math.Sqrt(2), turn + Math.PI / 4);
            Painter.DrawLine(Pens.Yellow, minimumSide * bigSide, turn + Math.PI);
            Painter.DrawLine(Pens.Yellow, minimumSide * bigSide - minimumSide * smallSide, turn + Math.PI / 2);

            Painter.Change(minimumSide * smallSide, turn - Math.PI);
            Painter.Change(minimumSide * smallSide * Math.Sqrt(2), turn + 3 * Math.PI / 4);
        }

        public static void Draw(int width, int height, double angleOfRotation, Graphics graphic)
        {
            Painter.Initialize(graphic);

            var minimumSide = Math.Min(width, height);

            var diagonalLength = Math.Sqrt(2) * (minimumSide * bigSide + minimumSide * smallSide) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Painter.SetPosition(x0, y0);

            DrawASide(minimumSide, 0);
            DrawASide(minimumSide, -Math.PI / 2);
            DrawASide(minimumSide, Math.PI);
            DrawASide(minimumSide, Math.PI / 2);
        }
    }
}