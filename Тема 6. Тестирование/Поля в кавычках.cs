using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("'", 0, "", 1)]
        [TestCase("\"", 0, "", 1)]
        [TestCase("'a' b'", 0, "a", 3)]
        [TestCase("'a", 0, "a", 2)]
        [TestCase(@"'a\' b'", 0, "a' b", 7)]
        [TestCase("sx\"a'\"", 2, "a'", 4)]
        [TestCase(@"abc ""def g h", 4, "def g h", 8)]
        [TestCase("some body", 4, "body", 5)]
        [TestCase("links eins zweidreivier", 10, "zweidreivier", 13)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var finalString = new StringBuilder();
            var length = 1;
            for (var i = startIndex + 1; i < line.Length; i++)
            {
                length++;
                if (line[startIndex] == line[i] && line[i - 1] != '\\')
                {
                    break;
                }
                if (line[i] != '\\')
                {
                    finalString.Append(line[i]);
                }
            }
            return new Token(finalString.ToString(), startIndex, length);
        }
    }
}