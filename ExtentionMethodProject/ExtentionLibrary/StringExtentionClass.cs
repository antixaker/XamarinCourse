using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentionLibrary
{
    public static class StringExtentionClass
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ','.',',','!','?','-' },StringSplitOptions.RemoveEmptyEntries).Count();
        }
    }
}
