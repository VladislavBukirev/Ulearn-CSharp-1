using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestOrder = MakeTrivialPermutation(checkpoints.Length, checkpoints);
            return bestOrder;
        }

        private static int[] MakeTrivialPermutation(int size, Point[] checkpoints)
        {
            var checkedList = GetRecursion(new int[size], 0);
            var variableForMinLength = double.PositiveInfinity;
            var finalArray = new int[size];
            for (var i = 0; i < checkedList.Count; i++)
            {
                var way = checkedList[i].Split(' ');
                var wayInInt = new int[size];
                for (var j = 0; j < wayInInt.Length; j++)
                    wayInInt[j] = int.Parse(way[j]);
                var length = checkpoints.GetPathLength(wayInInt);
                if (length < variableForMinLength)
                {
                    variableForMinLength = length;
                    finalArray = wayInInt;
                }
            }
            return finalArray;
        }

        private static List<string> GetRecursion(int[] permutation, int position)
        {
            return MakePermutations(permutation, position, new List<string>());
        }
        
        public static List<string> MakePermutations(int[] permutation, int position, List<string> finalList)
        {
            var collectedString = new StringBuilder();
            CollectListPermitatuons(collectedString, permutation, position, finalList);
            for (int i = 0; i < permutation.Length; i++)
            {
                var found = false;
                for (int j = 0; j < position; j++)
                {
                    if (permutation[j] == i)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    continue;
                permutation[position] = i;
                MakePermutations(permutation, position + 1, finalList);
            }

            return finalList;
		}
		
        public static void CollectListPermitatuons
            (StringBuilder collectedString, int[] permutation, int position, List<string> finalList)
        {
            if (position == permutation.Length)
            {
                if (permutation[0] == 0)
                {
                    foreach (var element in permutation)
                        collectedString.Append(element + " ");
                    finalList.Add(collectedString.ToString());
                }
            }
		}
    }
}