using System.Collections.Generic;

namespace NFlex
{
    public class AsciiComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.CompareOrdinal(x, y);
        }
    }
}
