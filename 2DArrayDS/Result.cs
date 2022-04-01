using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace _2DArrayDS
{
    class Result
    {

        public Result(bool debug)
        {
            Testcases(debug);
        }

        private void Testcases(bool debug)
        {
            var input = new List<List<int>> { new List<int>{ 1, 1, 1, 0, 0, 0 }, new List<int>{0,1,0,0,0,0 }, new List<int>{ 1, 1, 1, 0, 0, 0 },
                new List<int>{0,0,2,4,4,0 }, new List<int>{0,0,0,2,0,0 }, new List<int>{0,0,1,2,4,0 } };
            var result = HourglassSum(input);
            Debug.PrintForDebug($"Maximum hourglass sum is {result}\n",debug);
            var newResult = HourglassSumOptimal(input,debug);
            Debug.PrintForDebug($"Maximum hourglass sum is {newResult}\n", debug);
        }

        //Brute Force A)

        //O(1) time ,O(1) space
        private static int CalculateHourglassSum(int[,] array, int centerRow, int centerCol)
        {
            if (centerRow < 1 || centerRow > 4 || centerCol < 1 || centerCol > 4)
            {
                throw new ArgumentOutOfRangeException($"The hourglass center cannot lie on array perimeter");
            }

            return array[centerRow, centerCol] + array[centerRow - 1, centerCol] + array[centerRow + 1, centerCol] +
                array[centerRow + 1, centerCol - 1] + array[centerRow + 1, centerCol + 1] + array[centerRow - 1, centerCol - 1] +
                array[centerRow - 1, centerCol + 1];

        }

        //O(16) time, O(1) space
        private static int HourglassSum(int[,] arr)
        {
            int max = -63; //lowest possible value is seven -9s in hourglass.
            int temp;
            if (arr.LongLength != 36)
            {
                throw new ArgumentOutOfRangeException($"The array must be a 6x6 2D matrix");
            }

            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    temp = CalculateHourglassSum(arr, i, j);
                    max = Math.Max(temp, max);
                }

            }




            return max;
        }


        //Brute Force B) Concise

        //Just for the hackerrank input: List<List<int>>
        //O(16) time, O(1) space
        private static int HourglassSum(List<List<int>> array)
        {
            int max = Int32.MinValue;
            int temp;
            for (int centerRow = 1; centerRow < 5; centerRow++)
            {
                for (int centerCol = 1; centerCol < 5; centerCol++)
                {
                    temp = array[centerRow][centerCol] + array[centerRow - 1][centerCol] + array[centerRow + 1][centerCol] +
                array[centerRow + 1][centerCol - 1] + array[centerRow + 1][centerCol + 1] + array[centerRow - 1][centerCol - 1] +
                array[centerRow - 1][centerCol + 1];
                    max = Math.Max(temp, max);
                }

            }
            return max;
        }


        //Optimal. O(16) time, O(1) space
        //This can be done in one loop if you notice which elements you actually need from the 6X6 array.
        //Keep eye on symmetries and / % operations.
        private static int HourglassSumOptimal(List<List<int>> array,bool debug)
        {
            int max = -63;
            int temp;

            for (int i = 0; i < 16; i++)
            {
                int centerRow = i / 4 + 1;
                int centerCol = i % 4 + 1;
                Debug.PrintForDebug($"[{centerRow}][{centerCol}]", debug);
                temp = array[centerRow][centerCol] + array[centerRow - 1][centerCol] + array[centerRow + 1][centerCol] +
           array[centerRow + 1][centerCol - 1] + array[centerRow + 1][centerCol + 1] + array[centerRow - 1][centerCol - 1] +
           array[centerRow - 1][centerCol + 1];
                max = Math.Max(temp, max);
            }

            return max;
        }
    }
}
