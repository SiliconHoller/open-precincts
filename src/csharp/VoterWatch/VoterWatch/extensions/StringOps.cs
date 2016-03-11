using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoterWatch.extensions
{
    public static class StringOps
    {
        public static bool nonEmpty(this string s)
        {
            return !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s);
        }
    }
}
