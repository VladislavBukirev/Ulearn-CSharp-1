using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            var middle = left +  (right - left) / 2;
            if (left == middle) 
                return left;
            if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) > 0)
                return GetLeftBorderIndex(phrases, prefix, middle, right);
            return GetLeftBorderIndex(phrases, prefix, left, middle);
        }
    }
}