using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace SherlockAndAnagrams
{
    /// <summary>
    /// SherlockAndAnagrams<br></br>
    /// Two strings are anagrams of each other if the letters of one string can be rearranged to form the other string.<br></br>
    /// Given a string, find the number of pairs of substrings of the string that are anagrams of each other. <br></br>
    /// Constraints:<br></br>
    /// 2&lt;length of s&lt;100<br></br>
    /// contains only lowercase letters in the range ascii[a-z]. 
    /// </summary>
    class Result
    {
        public Result(bool debug)
        {
            TestCases(debug);
        }

        private void TestCases(bool debug)
        {
            
            var testCases = new List<string> { "abba", "abcd", "cdcd", "ifailuhkqq", "kkkk", "ifailuhkqqhucpoltgtyovarjsnrbfpvmupwjjjfiwwhrlkpekxxnebfrwibylcvkfealgonjkzwlyfhhkefuvgndgdnbelgruel" };
            foreach (var testCase in testCases)
            {
                Console.WriteLine(testCase);
                var result = SherlockAndAnagrams(testCase, debug);
                Console.WriteLine($"Total anagrammatic substrings is {result}\n");
            }
        }


        /* Approach (1): O(n^3) time, O(n) space
         * Based on the constraint that all characters in s are lowercase ascii [a-z]
         * make use of the fact that for all anagrams of a given string the sum of their characters' ascii values will be the same
         * There is a catch here, since there could be strings whose ascii character sum are equal even though they are not anagrams
         * due to different letter combinations tha sum to the same number. Therefore you have to generate unique keys for each anagram to be safe.
         * Initialize a dictionary of fixed length equal to the number of all possible anagrams (thus avoid O(n) for .Add), (or not since n<=100)
         * and store all ascii anagram keys to the dictionary.
         * Finallly, iterate through the dictionary and for every value above 1, let it be k, there will be k*(k-1)/2 anagram pairs in the string
         * The total sum of all the above is the answer.
         */

        //NOTE: I believe the complexities are correct, but I could be misinterpreting something.

        //Time T([n*(n-1)/2]*[n]+[n*(n+1)/2]) => O(n^3), where n=string length. 
        //space O(n) where n = s.length
        private int SherlockAndAnagrams(string s, bool debug = false)
        {
            //Print all substrings, if you want
            if (debug)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    StringBuilder subStr = new StringBuilder(s.Length);
                    for (int j = i; j < s.Length; j++)
                    {
                        subStr.Append(s[j]);
                        Console.Write(subStr + "\n");
                    }
                }
            }

            //The total number of substrings in a string of length n is n*(n+1)/2
            int capacity = s.Length * (s.Length + 1) / 2;
            Dictionary<string, int> dict = new Dictionary<string, int>(capacity);

            for (int i = 0; i < s.Length; i++)
            {
                StringBuilder subStr = new StringBuilder(s.Length);
                for (int j = i; j < s.Length; j++)
                {
                    //Append will always be O(1).
                    subStr.Append(s[j]);
                    //Construct anagram key.
                    string key=AnagramKey(subStr,debug);

                    //Check for/add/update frequency of anagram key.
                    //ContainsKey, Add will always be O(1).
                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, 1);
                    }
                    else
                    {
                        dict[key]++;
                    }

                }
            }

            Debug.PrintForDebugDictKvp(dict, debug);

            int anagramPairs = 0;

            foreach (var kvp in dict)
            {
                if (kvp.Value>1)
                {
                    anagramPairs += kvp.Value * (kvp.Value - 1) / 2; //This will be the total number of anagram pairs that have the same
                }                                                    //unique anagram key.
            }          
            return anagramPairs;
        }

        //Time T(m+26+[26*4]) where m: substring.length 
        //m comes from the first loop, 26 from the second loop, 26*4 from the ToString() method
        //=> At worst that is T(n+26*5) where n: string.length, which I believe goes to O(n)
        //Space T(26*4) => O(1)
        private string AnagramKey(StringBuilder subStr,bool debug)
        {
            int[] counter = new int[26];
            for (int i = 0; i < subStr.Length; i++)
            {
                counter[subStr[i] - 97]++;
            }
            StringBuilder anagramKey = new StringBuilder(26 * 4); //26 lowercase characters times (1 spot for letter plus 3 spots for
                                                                  //frequency since at worst case the original string could be one duplicate char
                                                                  // [char][char]....[char] where the char has maximum frequency of 100.
                                                                  //in general we would need to set size at log_10(string.length)+1
            for (int i = 0; i < 26; i++)
            {
                anagramKey.Append(Convert.ToChar(i + 97));
                anagramKey.Append(counter[i]);
            }
            Debug.PrintForDebug(anagramKey.ToString(),debug);
            return anagramKey.ToString();

        }



        //Questions : Can it be done in O(n^2) time O(n) space?, Am I using up excessive space?
        
    }
}
