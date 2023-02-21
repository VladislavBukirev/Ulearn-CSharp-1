using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }
        
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var actualCount = Math.Min(GetCountByPrefix(phrases, prefix), count);
            var result = new string[actualCount];
            for (var i = 0; i < actualCount; i++)
            {
                result[i] = phrases[startIndex + i];
            }
            return result;
        }
        
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var rightBorder = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return rightBorder - leftBorder - 1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [TestCase(new[]{"a", "ab", "abc", "bc" , "cd"}, "ab", 4, new[]{"ab", "abc"})]
        [TestCase(new string[0], "a", 5, new string[0])]
        [TestCase(new[]{"a", "ab", "abc", "bc" , "cd"}, "a", 4, new[]{"a", "ab", "abc"})]
        [TestCase(new[]{"a", "ab", "abc", "bc" , "cd"}, "a", 2, new[]{"a", "ab"})]
        public void GetTopByPrefix_Tests(string[] phrases, string prefix, int count, string[] expected)
        {
            var actual = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new[]{"b", "a", "ab", "abc", "bc"}, "", 5)]
        [TestCase(new string[0], "a", 0)]
        [TestCase(new[]{"a", "ab", "abc", "bc", "cd"}, "a", 3)]
        [TestCase(new[]{"a", "ab", "abc", "bc", "cd"}, "e", 0)]
        public void GetCountByPrefix_Tests(string[] phrases, string prefix, int expected)
        {
            var actual = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expected, actual);
        }
    }
}