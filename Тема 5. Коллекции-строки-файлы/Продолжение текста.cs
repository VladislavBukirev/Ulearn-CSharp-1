using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            for (var i = 0; i < wordsCount; i++)
            {
                var phrase = phraseBeginning.Split(' ');
                if (phrase.Length > 1 &&
                    nextWords.ContainsKey(phrase[phrase.Length - 2] + " " + phrase[phrase.Length - 1]))
                    phraseBeginning += " " + nextWords[phrase[phrase.Length - 2] +
                                                       " " + phrase[phrase.Length - 1]];
                else if (nextWords.ContainsKey(phrase[phrase.Length - 1]))
                    phraseBeginning += " " + nextWords[phrase[phrase.Length - 1]];
                else
                    break;
            }
            return phraseBeginning;
        }
    }
}