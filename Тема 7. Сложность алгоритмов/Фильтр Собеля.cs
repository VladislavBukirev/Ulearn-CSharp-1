using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] image, double[,] sx)
        {
            var width = image.GetLength(0);
            var height = image.GetLength(1);
            var sy = TransposeMatrix(sx);
            var result = new double[width, height];
            var compressionX = sx.GetLength(0) / 2;
            var compressionY = sx.GetLength(1) / 2;

            for (var x = compressionX; x < width - compressionX; x++)
            for (var y = compressionY; y < height - compressionY; y++)
            {
                var gx = ConvoluteMatrix(image, sx, x, y, compressionX);
                var gy = ConvoluteMatrix(image, sy, x, y, compressionY);

                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
            return result;
        }

        public static double[,] TransposeMatrix(double[,] originalMatrix)
        {
            var width = originalMatrix.GetLength(0);
            var height = originalMatrix.GetLength(1);
            var transposedMatrix = new double[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    transposedMatrix[x, y] = originalMatrix[y, x];
                }
            }

            return transposedMatrix;
        }

        public static double ConvoluteMatrix(double[,] image, double[,] matrix, int x, int y, int compression)
        {
            var result = 0.0;
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    result += matrix[i, j] * image[x - compression + i, y - compression + j];
                }
            }

            return result;
        }
    }
}