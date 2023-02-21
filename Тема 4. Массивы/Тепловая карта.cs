using System;
using System.Xml.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static string[] AddAxe(int axis, int plusNumber)
        {
            var axe = new string[axis];
            for (var i = 0; i < axis; i++)
                axe[i] = Convert.ToString(i + plusNumber);
            return axe;
        }

        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var date = AddAxe(30, 2);
            var months = AddAxe(12, 1);
            var axis = new double[date.Length, months.Length];
            foreach (var people in names)
                if (people.BirthDate.Day != 1) axis[people.BirthDate.Day - 2, people.BirthDate.Month - 1]++;
            return new HeatmapData("Пример карты интенсивностей", axis, date, months);
        }
    }
}