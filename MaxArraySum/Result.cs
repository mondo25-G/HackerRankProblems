using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace MaxArraySum
{
    /// <summary>
    /// Given an array of integers, find the subset of non-adjacent elements with the maximum sum. <br></br>
    /// Calculate the sum of that subset. It is possible that the maximum sum is 0, <br></br>
    /// the case when all elements are negative. <br></br>
    /// Constraints:<br></br>
    /// 1&lt;=n&lt;=10^5<br></br>
    /// -10^4&lt;=arr[i]&lt;=10^4
    /// </summary>
    class Result
    {
        private delegate int MaxSubsetSum(int[] arr);
        public Result(bool debug)
        {
            Debug.PrintForDebug($"DP with tabulation", debug);
            TestCases(MaxSubsetSumTabulation,debug);
            Debug.PrintForDebug($"DP with three variables", debug);
            TestCases(MaxSubsetSumThreeVariables, debug);
            Debug.PrintForDebug($"DP in place", debug);
            TestCases(MaxSubsetSumInPlace, debug);
        }

        private void TestCases(MaxSubsetSum del, bool debug)
        {
            List<int[]> testCases = new List<int[]>
            {
                new int[] {-2,1,3,-4,5},
                //[-2,3,5] = 6, [-2,3]=1, [-2,4]=-6
                //[-2,5]=3, [1,-4]=-3,[1,5]=6,[3,5]=8
                //maximum subset sum is 8. Any individual 
                //element is a subset as well.
                new int[] {-2,-3,-1},
                //In this case it is best not to choose any
                //element, return 0.
                new int[] {3,7,4,6,5},
                //[3,4,5],[3,4],[3,6],[3,5],[7,6],[7,5],[4,5]
                //The largest sum  is [7,6]=13
                new int[] {3,5,-7,8,10},
                //[3,-7,10],[3,8],[3,10],[5,8],[5,10],[-7,10]
                //The maximum subset sum is [5,10].
                new int[]{2,1,5,8,4}
                //[2,5,4],[2,5],[2,8],[2,4],[1,8],[1,4],[5,4]
                //Maximum subset sum: [2,5,4]=11.
               
            };


            foreach (var testcase in testCases)
            {
                Debug.PrintIEnumerable(testcase, debug);
                var result = del(testcase);
                Debug.PrintForDebug($"Max array sum is {result}", debug);
            }

            

        }
            //Approach: Dynamic programming with TABULATION
            /* arr = [-2,1,3,-4,5]
             * 
             * Create a new array to store the maximum subset sums.
             * 
             * dp = [...]
             * Base cases:
             * dp[0] = Math.Max(0,arr[0])
             * To figure out if we start from index 0 or index 1
             * dp[1] = Math.Max(dp[0],arr[1])
             * 
             * for i=2 to arr.length-1
             * 
             *You must always add the previous non-adjacent subarray sum
             * dp[i] = Math.Max(dp[i-1], arr[i]+ dp[i-2])
             * 
             * 
             */

            //Time complexity: O(n), Auxiliary space O(n)
            private static int MaxSubsetSumTabulation(int[] arr)
            {
                if (arr.Length==1)
                {
                    return arr[0] >= 0 ? arr[0] : 0;
                }

                int[] tabulation = new int[arr.Length];
                tabulation[0] = Math.Max(0, arr[0]);
                tabulation[1] = Math.Max(tabulation[0], arr[1]);


                for (int i = 2; i < arr.Length; i++)
                {
                    tabulation[i] = Math.Max(tabulation[i - 1], arr[i] + tabulation[i - 2]);
                }

                return tabulation[arr.Length - 1];
            }


            //Dynamic Programming with three variables.
            //Time complexity: O(n), Auxiliary space O(1)
            private static int MaxSubsetSumThreeVariables(int[] arr)
            {
                if (arr.Length == 1)
                {
                    return arr[0] >= 0 ? arr[0] : 0;
                }

                
                int twoPositionsBack = Math.Max(0, arr[0]);
                int onePositionBack = Math.Max(twoPositionsBack, arr[1]);
                int max = Int32.MinValue;

                for (int i = 2; i < arr.Length; i++)
                {
                   max = Math.Max(onePositionBack, arr[i] + twoPositionsBack);
                   twoPositionsBack = onePositionBack;
                   onePositionBack = max;
                }

                return max;
            }


            //Dynamic Programming with in place.
            //Time complexity: O(n), Auxiliary space O(1)
            private static int MaxSubsetSumInPlace(int[] arr)
            {
                if (arr.Length == 1)
                {
                    return arr[0] >= 0 ? arr[0] : 0;
                }


                arr[0] = Math.Max(0, arr[0]);
                arr[1]= Math.Max(arr[0], arr[1]);
                

                for (int i = 2; i < arr.Length; i++)
                {
                    arr[i] = Math.Max(arr[i-1], arr[i] + arr[i-2]);
                   
                }

                return arr[arr.Length-1];
            }

    }
}
