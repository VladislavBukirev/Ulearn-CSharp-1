namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            var divisionByTen = count % 10;
            var divisionByHundred = count % 100;
            if (divisionByTen == 1 && divisionByHundred != 11) return "рубль";
            if (divisionByHundred >= 12 && divisionByHundred <= 14) return "рублей";
            if (divisionByTen >= 2 && divisionByTen <= 4) return "рубля";	
            return "рублей";
        }
    }
}