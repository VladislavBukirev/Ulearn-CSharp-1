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