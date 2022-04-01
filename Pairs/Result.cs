using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace Pairs
{
    /// <summary>
    /// Given an array of integers (length n) and a target value (k), determine the number of pairs of array elements<br></br>
    /// that have a difference equal to the target value. <br></br>
    /// Constraints:<br></br>
    /// 2&lt;=n&lt;=10^5<br></br>
    /// 0&lt;=k&lt;=10^9<br></br>
    /// 0&lt;=arr[i]&lt;=2^31-1<br></br>
    /// Each integer arr[i] will be unique
    /// </summary>
    class Result
    {
        // Constraints:
        // 2<=n<=10^5
        // 0<=k<=10^9
        // 0<=arr[i]<=2^31-1
        // Each integer arr[i] will be unique

        public Result(bool debug)
        {
            TestCases(debug);
        }

        private static void TestCases(bool debug)
        {
            var testCases = new List<Tuple<int, List<int>>>
            {
                new Tuple<int, List<int>>(2,new List<int>{1, 5 ,3 ,4, 2,2})
            };
            foreach (var testCase in testCases)
            {
                Debug.PrintIEnumerable(testCase.Item2, debug);
                var result = PairsHashSet(testCase.Item1, testCase.Item2);
                Debug.PrintForDebug($"Number of array pairs whose difference is {testCase.Item1} : {result}", debug);

            }

        }
        //I'm not interested in any approach that involves sorting. It will only cost more.
        //Note: Since "Each integer arr[i] will be unique" using a hashet makes sense. 
        //If that wasn' t true however, I would have to use a dictionary.

        /*Fill Hashset with array values.
         *Iterate the array and look for arr[i]-k
        */

        //Approach : Hashset, Time Complexity O(n), Auxiliary Space O(n).

        private static int PairsHashSet(int k, List<int> arr)
        {
            int pairs = 0;
            HashSet<int> hs = new HashSet<int>(arr); //O(n) time, O(n) space

            for (int i = 0; i < arr.Count; i++) //O(n)
            {
                if (hs.Contains(arr[i] - k))//O(1)
                {
                    pairs++;
                }
            }

            return pairs;
        }

        //Approach: Hashset/Linq, Time Complexity O(n), Auxiliary Space O(n). Less code
        private static int PairsHashSetLinq(int k, List<int> arr)
        {
            
            HashSet<int> hs = new HashSet<int>(arr); //O(n) time, O(n) space

            return arr.Count(x => hs.Contains(x - k)); //O(n) time, O(1) space.
        }

    }
}
