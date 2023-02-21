using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var newPaint = new double[width,height];
            var treshholdValue = GetTreshholdValue(original, width, height, whitePixelsFraction);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (original[x, y] >= treshholdValue)
                        newPaint[x, y] = 1.0;
                    else
                        newPaint[x, y] = 0.0;
                }
            }
            return newPaint;
        }

        public static double GetTreshholdValue(double[,] original, int width, int height, double whitePixelsFraction)
        {
            var listPixels = GetTheDescendingList(original, width, height);
            var numberOfWhitePixels = (int)(whitePixelsFraction * width * height);
            return numberOfWhitePixels == 0
                ? double.PositiveInfinity
                : listPixels[numberOfWhitePixels - 1];
        }

        public static List<double> GetTheDescendingList(double[,] original, int width, int height)
        {
            var listPixels = new List<double>();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    listPixels.Add(original[x, y]);
                }
            }
            listPixels.Sort();
            listPixels.Reverse();
            return listPixels;
        }
    }
}