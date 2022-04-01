using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace FrequencyQueries
{
    /// <summary>
    /// Frequency Queries<br></br>
    /// You are given n queries. Each query is of the form two integers described below:<br></br>
    /// 1 x: Insert x in your data structure.<br></br>
    /// 2 y: Delete one occurence of y from your data structure, IF PRESENT.<br></br>
    /// 3 z:Check if any integer is present whose frequency is exactly z. If yes, print 1 else 0.<br></br>
    /// he queries are given in the form of a 2-D array of size where queries[i,0] contains the operation, <br></br>
    /// and queries[i,1] contains the data element.<br></br>
    /// Constraints:<br></br>
    /// 1&lt;=q&lt;=10^5<br></br>
    /// 1&lt;=x,y,z&lt;=10^9<br></br>
    /// All queries[i,0] ε (1,2,3) <br></br>
    /// 1&lt;=queries[i,0]&lt;=10^9
    /// </summary>
    class Result
    {
        public Result(bool debug)
        {
            TestCases(debug);
        }

        private void TestCases(bool debug)
        {
            //var testCases = new List<List<int>> { new List<int> {1,1 },new List<int> { 2,2}, new List<int> {3,2 },
            //new List<int> {1,1 }, new List<int> {1,1 }, new List<int> {2,1 }, new List<int> {3,2 }};


            var testCases2 = new List<List<int>> { new List<int> { 1, 1 }, new List<int> { 1, 2 } , new List<int> { 1, 1 }, new List<int> {3,14 },new List<int> {3,2 },new List<int> {1,1 }, new List<int> { 1, 2 },
            new List<int> {1,2 }, new List<int> {1,2 }, new List<int> {1,4 }, new List<int> {1,2 }, new List<int> {1,5 }, new List<int>{1,6 }, new List<int>{1,4 }, new List<int> {1,4 },
            new List<int> {2,2 }, new List<int> {2,2 }, new List<int>{2,2 }, new List<int> {3,14 } , new List<int> {3,2 } };


            //var result = FreqQueryInitial(testCases, debug)
            //Debug.PrintIEnumerable(result, debug);
            //Console.WriteLine("----");
            var result1 = FreqQuery(testCases2,debug);
            Debug.PrintIEnumerable(result1, debug);


        }


        /*First Approach. 
         *Let's try to disregard possible timeouts due to time complexity issues and break down the problem a bit. 
         *We shall need at least one Dictionary to store the numbers and their respective frequencies
         *We can iterate through the input once and look for all queries of type [1,x] (i.e insertions) in order to capture the
         *capacity of the dictionary beforehand. Thus we gain the advantage of O(1) time complexity
         *for dictionary operations .Add / .ContainsKey / .Remove which we almost certainly will need.
         *We shall also need a List to store the result we print. We can similarly loop through the input once (
         *using the same loop for the dictionary before)
         *and look for all queries of type [3,z] (i.e print) in order to capture the capacity of the list beforehand
         *Thus we gain the advantage of O(1) time complexity fot list operation .Add
         *It is not difficult afterwards to code the logic of the problem inside a second successive loop.
         *This approach is just fine, but there will be time complexity issues when we have to check for type [3,z]
         *operations. See the method below.
         */


        private static List<int> FreqQueryInitial(List<List<int>> queries, bool debug)
        {
            int capacityDict = 0;
            int capacityResult = 0;
            foreach (var list in queries)
            {
                if (list[0]==1)
                {
                    capacityDict++;
                }
                if (list[0] == 3)
                {
                    capacityResult++;
                }
            }

            Dictionary<int, int> frequenciesOfNums = new Dictionary<int, int>(capacityDict);
            List<int> result = new List<int>(capacityResult);

            foreach (var list in queries)
            {
                if (list[0]==1)
                {
                    if (!frequenciesOfNums.ContainsKey(list[1]))
                    {
                        frequenciesOfNums.Add(list[1], 1);
                    }
                    else
                    {
                        frequenciesOfNums[list[1]]++;
                    }
                }

                if (list[0]==2)
                {
                    if (frequenciesOfNums.ContainsKey(list[1]))
                    {
                        if (frequenciesOfNums[list[1]]>1)
                        {
                            frequenciesOfNums[list[1]]--;
                        }
                        else
                        {
                            frequenciesOfNums.Remove(list[1]);
                        }
                    }
                }

                if (list[0]==3)
                {
                    /* The problem lies in the following if condition. frequenciesOfNums.Values.Contains()
                     * is an O(n) operation and there is no way around it (given our analysis so far) making
                     * the total time complexity O(n^2) and for the given constraints, max[q]=10^5
                     * it is a very real possibility that you will get a TIMEOUT. What if all queries are of type [1]
                     * (additions?). Is there a way around this problem? See next method.
                     * 
                     */

                    if (frequenciesOfNums.Values.Contains(list[1]))
                    {
                        Console.WriteLine(1);
                        result.Add(1);
                    }
                    else
                    {
                        Console.WriteLine(0);
                        result.Add(0);
                    }
                }
                Debug.PrintForDebugDictKvp(frequenciesOfNums, debug);

            }
            
            return result;
        }

        /* One solution would be to use a second dictionary. You could store the actual frequencies you encounter
         * as keys, and the frequency you encounter those frequencies as values. You could carefully insert the
         * code logic in the previous method, thus when you check for type [3,z] operations you would use
         * the Dictionary .ContainsKey() method which could be made to be an O(1) operation at all times if
         * you ensure that the second dictionary will never resize on its own. Is it possible to know the 
         * size of this dictionary beforehand? The answer is yes. It will be (at most) equal to the number
         * of type [1,x] operations. Consider the extreme case of adding 5000 times the same number.
         * You will need one key for every time you insert the number. All other cases will lead to something smaller
         * in size. 
         */


        /* The code is a bit brutal, and could possibly use some refactoring .
         * Extra care needs to be taken to ensure that after each operation on the first dictionary, the second dictionary is properly updated.
         * But it runs in O(n) time, O(n) space.
         */

        private static List<int> FreqQuery(List<List<int>> queries, bool debug)
        {
            int capacityDict = 0;
            int capacityResult = 0;
            foreach (var list in queries)
            {
                if (list[0] == 1)
                {
                    capacityDict++;
                }
                if (list[0] == 3)
                {
                    capacityResult++;
                }
            }

            Dictionary<int, int> frequenciesOfNums = new Dictionary<int, int>(capacityDict);
            Dictionary<int, int> frequenciesOfFreqs = new Dictionary<int, int>(capacityDict);
            List<int> result = new List<int>(capacityResult);

            foreach (var list in queries)
            {
                //If I have addition
                if (list[0] == 1)
                {
                    //if dictionary doesn't contain the value
                    if (!frequenciesOfNums.ContainsKey(list[1]))
                    {
                        //if frequency dictionary doesn't contain frequency:1
                        if (!frequenciesOfFreqs.ContainsKey(1))
                        {
                            //add it
                            frequenciesOfFreqs.Add(1, 1);
                        }
                        else
                        {
                            // increment its frequency
                            frequenciesOfFreqs[1]++;
                        }
                        //add the value
                        frequenciesOfNums.Add(list[1], 1);
                    }
                    else //if dictionary contains the value
                    {
                        //Decrement the frequency of its frequency by 1.
                        frequenciesOfFreqs[frequenciesOfNums[list[1]]]--;

                        //If new frequency of frequency is below 1 
                        if (frequenciesOfFreqs[frequenciesOfNums[list[1]]]<1)
                        {
                            //remove it altogether.
                            frequenciesOfFreqs.Remove(frequenciesOfNums[list[1]]);
                        }

                        //If dictionary of frequencies doesn't contain the next frequency
                        if (!frequenciesOfFreqs.ContainsKey(frequenciesOfNums[list[1]]+1))
                        {
                            //Add it
                            frequenciesOfFreqs.Add(frequenciesOfNums[list[1]] + 1,1);
                        }
                        else
                        {
                            //or increment it
                            frequenciesOfFreqs[frequenciesOfNums[list[1]] + 1]++;
                        }

                        //Finally increment the value's frequency in the proper dictionary
                        frequenciesOfNums[list[1]]++;
                    }
                    
                }
                //If I have removal
                if (list[0] == 2)
                {
                    //if proper dictionary contains the value to remove
                    if (frequenciesOfNums.ContainsKey(list[1]))
                    {
                        //Decrement the frequency of its frequency by 1.
                        frequenciesOfFreqs[frequenciesOfNums[list[1]]]--;

                        //If new frequency of frequency is below 1 
                        if (frequenciesOfFreqs[frequenciesOfNums[list[1]]] < 1)
                        {
                            //remove it altogether.
                            frequenciesOfFreqs.Remove(frequenciesOfNums[list[1]]);
                        }

                        //If dictionary of frequencies doesn't contain the previous frequency 
                        if (!frequenciesOfFreqs.ContainsKey(frequenciesOfNums[list[1]] - 1))
                        {
                            //Add it but only if THE PREVIOUS FREQUENCY IS AT LEAST 1.
                            if (frequenciesOfNums[list[1]] - 1 >= 1)
                            {
                                frequenciesOfFreqs.Add(frequenciesOfNums[list[1]] - 1, 1);
                            }                           
                        }
                        else
                        {
                            //or increment it
                            frequenciesOfFreqs[frequenciesOfNums[list[1]] - 1]++;
                        }

                        //Finally decrement the value's frequency in the proper dictionary
                        frequenciesOfNums[list[1]]--;

                        //if its new frequency is below 1
                        if (frequenciesOfNums[list[1]]<1)
                        {
                            //remove it altogether.
                            frequenciesOfNums.Remove(list[1]);
                        }

                    }
                }
                //If I have to check existence of frequency
                if (list[0]==3)
                {
                    if (frequenciesOfFreqs.ContainsKey(list[1]))
                    {
                        Console.WriteLine(1);
                        result.Add(1);
                    }
                    else
                    {
                        Console.WriteLine(0);
                        result.Add(0);
                    }
                }
                Debug.PrintForDebug("value - frequency of value dictionary", debug);
                Debug.PrintForDebugDictKvp(frequenciesOfNums, debug);
                Debug.PrintForDebug("---", debug);
                Console.WriteLine("frequency of value - frequency of (frequency of value) dictionary", debug);
                Debug.PrintForDebugDictKvp(frequenciesOfFreqs, debug);
                Console.WriteLine("---\n");
            }
            return result;
        }
    }
}
