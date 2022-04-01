using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;


namespace GreedyFlorist
{
    /// <summary>
    /// A group of friends want to buy a bouquet of folwers. The florist wants to<br></br>
    /// maximize his number of new customers and the money he makes. To do this, <br></br>
    /// he decides he'll multiply the price of each flower by the number of that customer's<br></br>
    /// previouslu purchased flowers plus 1. The first flower will be original price, <br></br>
    /// (O+1)x original price, the next will be (1+1)xoriginal price and so on <br></br>
    /// Given the size of the group of friends (k), the number of flowers they want to <br></br>
    /// purchase (n) (= c.length) and the original prices of the flowers (array c)<br></br>
    /// determine the minimum cost to purchase all the flowers. The number of flowers they want<br></br>
    /// equals the length of the c array.<br></br>
    /// Constraints:<br></br>
    /// 1&lt;= n,k &lt;=100<br></br>
    /// 1&lt;= c[i]&lt;=10^6<br></br>
    ///  answer&lt;=2^31<br></br>
    ///   0&lt;= i&lt;=n
    /// </summary>
    class Result
    {

        public Result(bool debug)
        {
            TestCases(debug);
        }

        private static void TestCases(bool debug)
        {
            var testCases = new List<Tuple<int, int[]>>
            {
                new Tuple<int, int[]>(3,new int[]{2,5,6 }),
                new Tuple<int, int[]>(2,new int[]{2,5,6 }),
                new Tuple<int, int[]>(3,new int[]{1,3,5,7,9 })


            };
            foreach (var testCase in testCases)
            {
                Debug.PrintForDebug($"Friends {testCase.Item1}", debug);
                Debug.PrintIEnumerable(testCase.Item2, debug);
                var result = GetMinimumCostOptimal(testCase.Item1, testCase.Item2);
                Debug.PrintForDebug($"Minimum cost: {result}", debug);
            }
        }

        /* We need to use a greedy approach in such a way that the total cost the friends have to pay is minimized.
         * Let's break down the problem into some cases:
         * if number of friends >= number of flowers the total cost will simply be the sum of the cost of the flowers.
         * Each will buy one flower until the flower array is empty. 
         * If number of friends < number of flowers, then one or more of them will have to buy more than one flowers
         * until the flower array is empty.
         * Before that happens, it is in their best interest to buy flowers in order of decending price
         * until every friend has bought their first flower.
         * They should then keep buying the remaining ones in order of decending price such that for each friend
         * (number of previously purchased flower + 1) x price of current flower is at a minimum.
         * They should keep doing that until every friend has bought his 2nd,3rd,..,nth flower and stop
         * when the array is empty.
         * 
         */



        //Time Complexity:O(nlog(n)
        //Auxiliary Space: O(n)
        private static int GetMinimumCost(int k, int[] c)
        {
            IOrderedEnumerable<int> orderedDesc = c.OrderByDescending(x => x); //Time O(nlog(n), Space O(n).
            int counter = k;
            int previousPurchases = 0;
            int sum = 0;

            foreach (var flower in orderedDesc)
            {
                counter--;
                sum += (previousPurchases + 1) * flower;
                if (counter==0)
                {
                    counter = k;
                    previousPurchases++;
                }
            }
            return sum;
        }

        //Improvement.
        //Time Complexity:O(nlog(n)
        //Auxiliary Space: O(1)
        private static int GetMinimumCostOptimal(int k, int[] c)
        {
            Array.Sort(c); //O(nlog(n));
            int counter = k;
            int previousPurchases = 0;
            int sum = 0;

            for (int i=c.Length-1; i>=0; i-- )
            {
                counter--;
                sum += (previousPurchases + 1) * c[i];
                if (counter == 0)
                {
                    counter = k;
                    previousPurchases++;
                }
            }
            return sum;
        }
    }
}
