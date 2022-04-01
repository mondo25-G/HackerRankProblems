using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimumAbsoluteDiifferenceInArray
{
    //Given an array 'arr' of 'n' integers, find the minimum absolute difference between any two elements in the array. 

    /* Constraints:
     * 2<=n<=10^5
     * 10^-9<=arr[i]<=10^9
     */
    class Result
    {
        public Result()
        {
            Console.WriteLine(MinimumAbsoluteDifference(new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4, 6, 6 }));
        }

        public static int MinimumAbsoluteDifference(List<int> arr)
        {
            int size = arr.Count;
            int min = Int32.MaxValue;
            arr.Sort();//O(NlogN)
            for (int i = 1; i < size; i++) //O(N)
            {
                if (Math.Abs(arr[i] - arr[i - 1]) < min)
                {
                    min = Math.Abs(arr[i] - arr[i - 1]);
                }

            }
            return min;
        }
    }
}
