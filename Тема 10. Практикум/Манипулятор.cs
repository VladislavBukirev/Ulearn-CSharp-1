using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowX = Manipulator.UpperArm * Math.Cos(shoulder);
            var elbowY = Manipulator.UpperArm * Math.Sin(shoulder);
            var elbowPos = new PointF((float) elbowX, (float) elbowY);

            var forearmAngle = shoulder + elbow - Math.PI;
            var forearmX = elbowX + Manipulator.Forearm * Math.Cos(forearmAngle);
            var forearmY = elbowY + Manipulator.Forearm * Math.Sin(forearmAngle);
            var wristPos = new PointF((float) forearmX, (float) forearmY);

            var palmAngle = forearmAngle + wrist - Math.PI;
            var palmX = forearmX + Manipulator.Palm * Math.Cos(palmAngle);
            var palmY = forearmY + Manipulator.Palm * Math.Sin(palmAngle);
            var palmEndPos = new PointF((float) palmX, (float) palmY);
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, 180, 150)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, 120, 90)]
        [TestCase(Math.PI, Math.PI, Math.PI, -330, 4.041200967613845E-14d)]
        [TestCase(Math.PI / 3, Math.PI / 4, Math.PI / 2, 
            48.102737426757813, -1.5364313125610352)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }
    }
}