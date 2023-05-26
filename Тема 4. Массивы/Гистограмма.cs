using System;
using System.Linq;
using System.Windows.Forms;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var date = new string[31];
            for (var i = 0; i < 31; i++)
                date[i] = Convert.ToString(i + 1);

            var countsOfBirthdays = new double[date.Length];
            for (var j = 0; j < date.Length; j++)
            {
                foreach (var people in names)
                    if (people.BirthDate.Day != 1 && people.BirthDate.Day == j + 1 && people.Name == name)
                        countsOfBirthdays[j]++;
            }
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                date,
                countsOfBirthdays);
        }
    }
}