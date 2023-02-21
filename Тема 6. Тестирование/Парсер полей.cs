using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] {"text"})]
        [TestCase("", new string[0])]
        [TestCase("\'\'", new[] { "" })]
        [TestCase("\"'\"", new[] {"'"})]
        [TestCase(" start space", new[]{"start", "space"})]
        [TestCase("hello world", new[] {"hello", "world"})]
        [TestCase("hello  world", new[] {"hello", "world"})]
        [TestCase("' ", new[] {" "})]
        [TestCase("'\"\"", new[] { "\"\"" })]
        [TestCase(@"""\\""", new[] {"\\"})]
        [TestCase(@"'\'", new[] {@"'"})]
        [TestCase(@"""\""", new[] {@""""})]
        [TestCase(@"abc """"", new[]{"abc" , ""})]
        [TestCase(@""""" abc", new[]{"", "abc"})]
        [TestCase(@"omg""yavse", new[]{"omg", "yavse"})]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            Token token;
            var tokens = new List<Token>();
            for (var i = 0 ; i < line.Length; i++)
            {
                if(line[i] == ' ')
                    continue;
                if (line[i] == '\'' || line[i] == '\"')
                    token = ReadQuotedField(line, i);
                else
                    token = ReadSimpleField(line, i);
                i = token.GetIndexNextToToken() - 1;
                tokens.Add(token);
            }
            return tokens; 
		}

        private static Token ReadSimpleField(string line, int startIndex)
        {
            var finalString = new StringBuilder();
            for (var i = startIndex; i < line.Length; i++)
            {
                //Отличить от поля в ковычках
                if (line[i] == '\'' || line[i] == '\"' || line[i] == ' ')
                    break;
                finalString.Append(line[i]);
            }
            return new Token(finalString.ToString(), startIndex, finalString.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}