using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            if (a < 0 || b < 0 || c < 0 || a + b < c || a + c < b || b + c < a)
                return double.NaN;
            return Math.Acos((a*a + b*b - c*c) / (2*a*b));
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(-1, 2, 3, double.NaN)]
        [TestCase(0, 1, 2, double.NaN)]
        [TestCase(1, 2, 100, double.NaN)]
        [TestCase(5, 5, 9, 2.2395390299972684d)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var actual = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, actual, 1e-5);
        }
    }
}