using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
         * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
         * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
         * заменяется на медиану всех цветов в некоторой окрестности пикселя.
         * https://en.wikipedia.org/wiki/Median_filter
         * 
         * Используйте окно размером 3х3 для не граничных пикселей,
         * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
         */
        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var newPaint = new double[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    newPaint[x, y] = GetMedianPixel(x, y, original);
                }
            }
            return newPaint;
        }

        public static double GetMedianPixel(int x, int y, double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var pixelsAround = new List<double>();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if(CheckPixelsAround(x-1+i, y-1+j, width, height))
                        pixelsAround.Add(original[x-1+i, y-1+j]);
                }
            }
            return TakeColorMedianPixel(pixelsAround);
        }

        public static double TakeColorMedianPixel(List<double> pixelsAround)
        {
            pixelsAround.Sort();
            return pixelsAround.Count % 2 == 1
                ? pixelsAround[pixelsAround.Count / 2]
                : (pixelsAround[pixelsAround.Count / 2 - 1] + pixelsAround[pixelsAround.Count / 2]) / 2;
        }
		
        public static bool CheckPixelsAround(int x, int y, int width, int height)
        {
            return x >= 0 && y >= 0 && x < width && y < height;
        }
    }
}