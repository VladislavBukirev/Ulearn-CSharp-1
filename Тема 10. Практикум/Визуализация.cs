using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Manipulation
{
	public static class VisualizerTask
	{
		public static double X = 220;
		public static double Y = -100;
		public static double Alpha = 0.05;
		public static double Wrist = 2 * Math.PI / 3;
		public static double Elbow = 3 * Math.PI / 4;
		public static double Shoulder = Math.PI / 2;

		public static Brush UnreachableAreaBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 230));
		public static Brush ReachableAreaBrush = new SolidBrush(Color.FromArgb(255, 230, 255, 230));
		public static Pen ManipulatorPen = new Pen(Color.Black, 3);
		public static Brush JointBrush = Brushes.Gray;

		public static void KeyDown(Form form, KeyEventArgs key)
		{
			var addedValue = 0.1;
			switch (key.KeyCode)
				{
					case(Keys.Q):
						Shoulder += addedValue;
						break;
					case(Keys.A):
						Shoulder -= addedValue;
						break;
					case(Keys.W):
						Elbow += addedValue;
						break;
					case(Keys.S):
						Elbow -= addedValue;
						break;
				}
			Wrist = - Alpha - Shoulder - Elbow;
			form.Invalidate(); 
		}


		public static void MouseMove(Form form, MouseEventArgs e)
		{
			var point = ConvertWindowToMath(new PointF(e.X, e.Y), GetShoulderPos(form));
			X = point.X;
			Y = point.Y;
			
			UpdateManipulator();
			form.Invalidate();
		}

		public static void MouseWheel(Form form, MouseEventArgs e)
		{
			Alpha += 0.05 * e.Delta;
			UpdateManipulator();
			form.Invalidate();
		}

		public static void UpdateManipulator()
		{
			var array = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
			if (!double.IsNaN(array[0]))
			{ 
				Shoulder = array[0];
				Elbow = array[1];
				Wrist = array[2];
			}
		}

		public static void DrawManipulator(Graphics graphics, PointF shoulderPos)
		{
			var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);
			DrawReachableZone(graphics, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);
			
			for (var i = 0; i < joints.Length; i++)
			{
				joints[i] = ConvertMathToWindow(joints[i], shoulderPos);
				if(i == 0)
					DrawLineAndEllipse(graphics, shoulderPos.X, shoulderPos.Y, joints[i].X, joints[i].Y);
				else
					DrawLineAndEllipse(graphics, joints[i-1].X, joints[i-1].Y, joints[i].X, joints[i].Y);
			}

			graphics.DrawString(
                $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}", 
                new Font(SystemFonts.DefaultFont.FontFamily, 12), 
                Brushes.DarkRed, 
                10, 
                10);
		}
		
		public static void DrawLineAndEllipse(Graphics graphics, float x1, float y1, float x2, float y2)
		{
			graphics.DrawLine(ManipulatorPen, x1, y1, x2, y2);
			graphics.FillEllipse(JointBrush, x1 - 5, y1 - 5, 10, 10);
		}
		
		private static void DrawReachableZone(
            Graphics graphics, 
            Brush reachableBrush, 
            Brush unreachableBrush, 
            PointF shoulderPos, 
            PointF[] joints)
		{
			var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
			var rmax = Manipulator.UpperArm + Manipulator.Forearm;
			var mathCenter = new PointF(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
			var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
			graphics.FillEllipse(reachableBrush, windowCenter.X - rmax, windowCenter.Y - rmax, 2 * rmax, 2 * rmax);
			graphics.FillEllipse(unreachableBrush, windowCenter.X - rmin, windowCenter.Y - rmin, 2 * rmin, 2 * rmin);
		}

		public static PointF GetShoulderPos(Form form)
		{
			return new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
		}

		public static PointF ConvertMathToWindow(PointF mathPoint, PointF shoulderPos)
		{
			return new PointF(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
		}

		public static PointF ConvertWindowToMath(PointF windowPoint, PointF shoulderPos)
		{
			return new PointF(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
		}
	}
}