using System;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var upperArm = Manipulator.UpperArm;
            var forearm = Manipulator.Forearm;
            var palm = Manipulator.Palm;

            var wristX = x - palm * Math.Cos(alpha);
            var wristY = y + palm * Math.Sin(alpha);
            var sideShoulderWrist = Math.Sqrt(wristX * wristX + wristY * wristY);
            var elbow = TriangleTask.GetABAngle(upperArm, forearm, sideShoulderWrist);

            var ray = Math.Atan2(wristY, wristX);
            var angleWrShElb = TriangleTask.GetABAngle(upperArm, sideShoulderWrist, forearm);
            var shoulder = angleWrShElb + ray;

            var wrist = - alpha - shoulder - elbow;     
            if(Math.Sqrt(x*x + y*y) <= upperArm + forearm + palm)
                return new[] { shoulder, elbow, wrist };
            return new[] { double.NaN, double.NaN, double.NaN };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            for (var i = 0; i < 100000; i++)
            {
                Random rnd = new Random();
                var x = (double)(rnd.Next(0, 400));
                var y = (double)(rnd.Next(0, 300));
                var alpha = (double)rnd.Next(-360, 360);
                var angles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
                var actual = AnglesToCoordinatesTask.GetJointPositions(angles[0], angles[1], angles[2]);
                if (!Double.IsNaN(actual[2].X))
                {
                    Assert.AreEqual(x, actual[2].X, 1e-4);
                    Assert.AreEqual(y, actual[2].Y, 1e-4);
                }
            }
        }
    }
}