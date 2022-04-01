using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace HashTablesRansomNote
{
    /// <summary>
    /// Hashtables:Ransom Note<br></br>
    /// Harold is a kidnapper who wrote a ransom note, but now he is worried it will be traced back to him through his handwriting. <br></br>
    /// He found a magazine and wants to know if he can cut out whole words from it and use them to create an untraceable replica <br></br>
    /// of his ransom note. The words in his note are case-sensitive and he must use only whole words available in the magazine. <br></br>
    /// He cannot use substrings or concatenation to create the words he needs.Given the words in the magazine and the words in <br></br>
    /// the ransom note, print Yes if he can replicate his ransom note exactly using whole words from the magazine; otherwise, print No.<br></br>
    /// Constraints<br></br>
    /// 1&lt; length of magazine, length or ransom note &lt; 30000<br></br>
    /// 1&lt; length of magazine word, length of ransom note word &lt;5<br></br>
    /// Each word consists of English alphabetic characters [a-z] [A-Z]
    /// </summary>
    class Result
    {
        public Result(bool debug)
        {
            TestCases(debug);
        }



        private void TestCases(bool debug)
        {
            CheckMagazine(new List<string> { "two", "times", "three", "is", "not", "four" },
                new List<string> { "two", "times", "two", "is", "four" }, debug);
            
            CheckMagazine(new List<string> { "give", "me", "one", "grand", "today", "tonight" },
                new List<string> { "give", "one", "grand", "today" }, debug);
        }

        // Create a dictionary for the magazine
        //Time O(n) (amortized), Space O(n) 

        private static void CheckMagazine(List<string> magazine, List<string> note,bool debug)
        {
            string answer = "Yes";
            Dictionary<string, int> magazineDict = new Dictionary<string, int>();
            foreach (var word in magazine)
            {
                if (!magazineDict.ContainsKey(word))
                {
                    magazineDict.Add(word, 1);
                }
                else
                {
                    magazineDict[word]++;
                }
            }

            foreach (var word in note)
            {
                if (!magazineDict.ContainsKey(word))
                {
                    answer = "No";
                    break;
                }
                else if (magazineDict.ContainsKey(word) && magazineDict[word]>0)
                {
                    magazineDict[word]--;                    
                }
                else
                {
                    answer = "No";
                    break;
                }
            }
            Debug.PrintForDebugDictKvp(magazineDict, debug);
            Console.WriteLine(answer);
        }
    }
}
