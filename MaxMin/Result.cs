using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace MaxMin
{
    /// <summary>
    /// You will be given a list of integers, arr of length n, and a single integer k. You must create an array of length  k from <br></br>
    /// such that its unfairness is minimized.Call that array arr'. Unfairness of an array is calculated as max(arr')-min(arr')
    /// where max denotes the largest integer in arr' and min denotes the smallest integer in arrr'.
    /// Constraints:<br></br>
    /// 2&lt;=n&lt;=10^5<br></br>
    /// 2&lt;=k&lt;=n<br></br>
    /// 0&lt;=arr[i]&lt;=10^9
    /// </summary>
    class Result
    {
        /*
         * Constraints:
         * 2<=n<=10^5
         * 2<=k<=n
         * 0<=arr[i]<=10^9
        */
        public Result(bool debug)
        {
            TestCases(debug);
        }


        public static void TestCases(bool debug)
        {
            var testCase = new List<int> { 4504, 1520, 5857, 4094, 4157, 3902, 822, 6643, 2422, 7288, 8245, 9948, 2822, 1784, 7802, 3142, 9739, 5629, 5413, 7232 };
            Debug.PrintIEnumerable(testCase, true);
            var result = MaxMin(5, testCase, debug);
            Console.WriteLine($"Unfairness of array is {result}");


        }

        //Without any serious thought you could sort the array and then always return arr[k - 1] - arr[0].
        //thinking that the [Maximum k-plet element - Minimum k-plet element] of the first k-plet would be the minimum.
        //That would be wrong however, because we haven't taken into account the fact that there could be other k-plets 
        // in this sorted array {arr_1,arr_2, ... ,arr_k}  such that [Maximum k-plet element - Minimum k-plet element] is minimized.
        //We need a greedy approach here.It obviously helps to sort the array, so we have to iterate through all the k-plets in the sorted array and find the minimum 
        //Sorting the array beforehand ensures that we only have to iterate SEQUENTIALLY all the k-plets to find 
        //the minimum [Maximum k-plet element - Minimum k-plet element],
        //as opposed to leaving it unsorted where we would have no clue how to form k-plets systematically.

        //Time Complexity O(nlog(n)), Auxiliary space O(1).
        public static int MaxMin(int k, List<int> arr,bool debug=false)
        {
            arr.Sort();
            int min = 0;
            min = arr[k - 1] - arr[0];
            Debug.PrintIEnumerable(arr, debug);
            for (int i = k-1; i < arr.Count; i++)
            {
                Debug.PrintForDebug($"{arr[i]-arr[i-(k-1)]}", debug);
                if (arr[i]-arr[i-(k-1)]<min)
                {
                    min = arr[i] - arr[i - (k - 1)];
                }
            }
            Debug.PrintForDebug($"---",debug);
            return min;
        }

        //TODO: Maybe there is a way to solve Max Min without .Sort() ?? Could this be done in O(n)?
    }
}
