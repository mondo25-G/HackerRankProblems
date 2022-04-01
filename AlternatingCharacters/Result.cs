using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternatingCharacters
{
    /// <summary>
    /// You are given a string containing characters A and B only.<br></br>
    /// Your task is to change it into a string such that there are no matching adjacent characters.<br></br>
    /// To do this, you are allowed to delete zero or more characters in the string.<br></br>
    /// Your task is to find the minimum number of required deletions.<br></br>
    /// Constraints:<br></br>
    /// Each string contains characters A and B only .<br></br>
    /// 1&lt;=string.Length&lt;=10^5
    /// </summary>
    class Result
    {
        public Result()
        {
            Console.WriteLine(AlternatingCharacters("ABBAABABABAAAAAABBBAABABB"));
        }

        public static int AlternatingCharacters(string s)
        {
            int count = 0;
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i+1]==s[i])
                {
                    count++;
                }
            }
            return count;
        }
    }
}
