public static double Calculate(string userInput)
{
    var values = userInput.Split();
    var amount = double.Parse(values[0]);
    var percent = double.Parse(values[1]);
    var months = int.Parse(values[2]);
    return amount * Math.Pow(1 + percent / 12 / 100, months);
}