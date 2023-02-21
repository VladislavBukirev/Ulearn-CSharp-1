using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var nGramms = FillInNGramms(text);

            var innerDict = new Dictionary<string, Dictionary<string, int>>();

            foreach (var words in nGramms)
                if (words.Count == 2) CountTheNGrammsEntrances(innerDict, words[0], words[1]);
                else if (words.Count == 3) CountTheNGrammsEntrances(innerDict, words[0] + " " + words[1],
                    words[2]);

            foreach (var items in innerDict)
                if (!result.ContainsKey(items.Key)) result[items.Key] = FindMaxKeyFrequency(items);

            return result;
        }

        private static List<List<string>> FillInNGramms(List<List<string>> text)
        {
            var nGramms = new List<List<string>>();
            foreach (var words in text)
            {
                if (words.Count > 2)
                    for (var i = 0; i < words.Count - 2; i++)
                        nGramms.Add(new List<string> { words[i], words[i + 1], words[i + 2] });
                if (words.Count > 1)
                    for (var i = 0; i < words.Count - 1; i++)
                        nGramms.Add(new List<string> { words[i], words[i + 1] });
            }

            return nGramms;
        }

        private static void CountTheNGrammsEntrances(Dictionary<string, Dictionary<string, int>> numberInNGramms,
            string firstWord, string secondWord)
        {
            if (!numberInNGramms.ContainsKey(firstWord))
            {
                numberInNGramms[firstWord] = new Dictionary<string, int>();
                if (!numberInNGramms[firstWord].ContainsKey(secondWord))
                    numberInNGramms[firstWord][secondWord] = 0;
            }
            else if (!numberInNGramms[firstWord].ContainsKey(secondWord))
                numberInNGramms[firstWord][secondWord] = 0;
            numberInNGramms[firstWord][secondWord]++;
        }

        private static string FindMaxKeyFrequency(KeyValuePair<string, Dictionary<string, int>> items)
        {
            var maxNumberKey = "";
            var countMaxKey = 0;

            foreach (var some in items.Value)
            {
                if (countMaxKey == some.Value)
                    if (string.CompareOrdinal(maxNumberKey, some.Key) < 0)
                        continue;
                    else 
                        maxNumberKey = some.Key;
                else if (countMaxKey < some.Value)
                {
                    countMaxKey = some.Value;
                    maxNumberKey = some.Key;
                }
            }

            return maxNumberKey;
        }
    }
}