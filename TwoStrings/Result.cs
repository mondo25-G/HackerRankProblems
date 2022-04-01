using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoStrings
{
    /// <summary>
    /// Given two strings, determine if they share a common substring. A substring may be as small as one character.<br></br>
    /// Constraints:<br></br>
    /// 1&lt;=|s1|,|s2|&lt;10^5<br></br>
    /// s1,s2 consist only of characters in the range ascii[a-z]
    /// </summary>
    class Result
    {
        /*
         * Constraints:
         * 1<=|s1|,|s2|<=10^5
         * s1,s2 consist only of characters in the range ascii[a-z]
         */
        public Result()
        {
           Console.WriteLine(TwoStrings("abcdee", "jkjkllss"));
        }

        private static string TwoStrings(string s1, string s2)
        {
            string result = "NO";
            HashSet<char> hs = new HashSet<char>(s1.Length);

            foreach (char c in s1)
            {
                hs.Add(c);
            }

            foreach (char c in s2)
            {
                if (hs.Contains(c))
                {
                    result = "YES";
                    break; //All you need is just one character. Says so in problem statement. Tricky huh?
                }
            }
            return result;
        }
    }
}
