using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace StringsMakingAnagrams
{
    /// <summary>
    /// Strings: Making Anagrams<br></br>
    /// Given two strings, a and  b, that may or may not be of the same length, <br></br>
    /// determine the minimum number of character deletions required to make and anagrams. <br></br>
    /// Any characters can be deleted from either of the strings. <br></br>
    /// Constraints<br></br>
    /// 1&lt;=a.length,b.length&lt;=10^4<br></br>
    /// The strings a and b consist of lowercase English alphabetic letters, ascii[a-z] <br></br>
    /// </summary>
    class Result
    {
        public Result()
        {
            var result = MakeAnagramRefactored("abadazefad", "abbadeeazadd");
            Console.WriteLine($"Minimum number of deletions to turn them into anagrams: {result}");
        }


        //Approach: Since both strings consist of lowercase english ascii [97-122] character, 
        // create 2 ascii character counters int[26]
        //Iterate through both strings to fill them, and finally iterate all lowercase characters (26) in both counters
        //storing the absolute differences between each lowercase english ascii character count. The total sum of those
        //differences is the answer.

        //O(n) time, O(1) space. Three loops, two counters
        public static int MakeAnagram(string a, string b)
        {
            
            int[] aCounter = new int[26];
            int[] bCounter = new int[26];

            for (int i = 0; i < a.Length; i++)
            {
                aCounter[a[i] - 97]++;
            }

            for (int i = 0; i < b.Length; i++)
            {
                bCounter[b[i] - 97]++;
            }
            int delete = 0;

            for (int i = 0; i < 26; i++)
            {
                delete += Math.Abs(aCounter[i] - bCounter[i]);
            }
            return delete;
        }

        //O(n) time, O(1) space. Refactored: Two loops, one counter.
        public static int MakeAnagramRefactored(string a, string b)
        {
            int maxLength = Math.Max(a.Length, b.Length);
            int[,] counter = new int[26,2];


            for (int i = 0; i < maxLength; i++)
            {
                try
                {
                    counter[a[i] - 97, 0]++;
                    counter[b[i] - 97, 1]++;
                }
                catch (IndexOutOfRangeException)
                {

                }
            }

            int delete = 0;

            for (int i = 0; i < 26; i++)
            {
                delete += Math.Abs(counter[i,0]-counter[i,1]);
            }
            return delete;
        }

    }
}
