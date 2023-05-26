using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Mime;
using System.Text;
using NUnit.Framework;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        static char[] separators = new char[] {'.', '?', '!', ';', ':', '(', ')'};
        public static List<string> GetWords(string sentence)
        {
            var wordsList = new List<string>();
            StringBuilder word = new StringBuilder();
            foreach (var ch in sentence.Trim())
            {
                if (char.IsLetter(ch) || ch == '\'')
                    word.Append(ch);
                else
                {
                    if (word.Length > 0)
                        wordsList.Add(word.ToString());
                    word.Clear();
                }
            }
            if(word.Length > 0)
                wordsList.Add(word.ToString());    
            return wordsList;
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentences = new List<string>(text.Split(separators,
                StringSplitOptions.RemoveEmptyEntries));
            foreach (var sentence in sentences)
            {
                var wordList = GetWords(sentence.ToLower());
                if(wordList.Count > 0)
                    sentencesList.Add(wordList);
            }
            return sentencesList;
        }
    }
}