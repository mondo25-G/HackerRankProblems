using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace SherlockAndTheValidString
{
    /// <summary>
    /// Sherlock considers a string to be valid if all characters of the string appear the same number of times. <br></br>
    /// It is also valid if he can remove just character at index in the string, <br></br>
    /// and the remaining characters will occur the same number of times.<br></br>
    /// Given a string , determine if it is valid. If so, return YES, otherwise return NO.<br></br>
    /// Constraints:<br></br>
    /// 1&lt=|s|&lt=10^5<br></br>
    /// s[i] in ascii(a-z)
    /// </summary>
    class Result
    {
        /*  
         *   Constraints:
         *   1<=|s|<=10^5
         *   s[i] in ascii(a-z)
         */
        public Result()
        {
            var testCases = TestCases();
            foreach (var testCase in testCases)
            {
                Console.WriteLine($"Is {testCase} a valid string?");
                var result = IsValid(testCase);
                Console.WriteLine($"ANSWER: {result}\n");
            }
        }
        private static List<string> TestCases()
        {
            List<string> testCases = new List<string>
            {
                "aaabbbb","bbbbcccggg","bbbbccccggg","abcddddd","aabbcd","abbbbcccceeeedddd","aaaaabbbbcccceeeedddd",
                "aaaabbbbcccceeeedddd","abbbbcccceeddd","b",""
            };
            return testCases;
        }
        private static string IsValid(string s)//O(n)
        {
            //If string has only one character and i remove it then it is a valid string. e.x. "a"
            if (s.Length == 1)
            {
                return "YES";
            }

            //Simple collection counter to store frequencies of lowercase alphabet in string.
            var collectionCounter = new int[26];
            foreach (var character in s)
            {
                collectionCounter[character - 97]++;
            }

            //Dictionary
            //KEY: frequency of appearance of char
            //VALUE: frequency of frequency of appearance of char
            var dictionary = new Dictionary<int, int>(s.Length);
            foreach (var frequency in collectionCounter)
            {
                if (frequency != 0)
                {
                    if (!dictionary.ContainsKey(frequency))
                    {
                        dictionary.Add(frequency, 1);
                    }
                    else
                    {
                        dictionary[frequency]++;
                    }
                }
            }
            //Print / Debug
            Debug.PrintForDebugDictKvp(dictionary, true);

            //If there is exactly 1 frequency of appearance present in dictionary, string is valid.
            //e.x. "abcd", "aabbccdd"
            int keysCount = dictionary.Keys.Count();
            if (keysCount == 1)
            {
                return "YES";
            }
            //If there are more than 2 frequencies of appearance present in dictionary is 1 , string is not valid
            //e.x. "abbccc", "aabbcccdddffff"
            if (keysCount > 2)
            {
                return "NO";
            }
            string answer = "NO";
            //if there are exactly two frequencies of appearance in dictionary
            if (keysCount == 2)
            {
                var minKey = dictionary.Keys.Min();
                var maxKey = dictionary.Keys.Max();
                //If the lowest frequency appearance of the two is exactly 1 and
                //ITS frequency of appearance is exactly one, the string is valid.
                //e.x. "abbbbcccc", "aaabccc", "aabbc"
                if (minKey == 1 && dictionary[minKey] == 1)
                {
                    answer = "YES";
                }
                //If the difference between the two frequencies of appearance is exactly one and
                //the frequency of the greatest of the two is exactly 1, the string is valid.
                //e.x. "aaabbbb" is valid but not "aaabbbbcccc".
                if (maxKey - minKey == 1 && dictionary[maxKey] == 1)
                {
                    answer = "YES";
                }

            }
            //In all other cases the string is not valid.
            return answer;
        }
    }
}
