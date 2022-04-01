using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace LuckBalance
{
    /* Lena is preparing for an important coding competition that is preceded 
         * by a number of sequential preliminary contests. 
         * Initially, HER LUCK BALANCE IS 0. She believes in "saving luck", and wants to check her theory.
         * Each contest is described by two integers, L[i] and T[i]:
         * 
         * L[i] is the amount of LUCK associated with a CONTEST. 
         * IF Lena WINS the contest, her LUCK BALANCE will DECREASE by L[i].
         * IF Lena LOSES the contest, her LUCK BALANCE will INCREASE by L[i].
         * 
         * T[i] denotes the CONTEST's IMPORTANCE rating. 
         * It's EQUAL TO 1 if the contest is IMPORTANT. 
         * IT's EQUAL TO 0 if the contest is UNIMPORTANT.
         * 
         * If Lena LOSES NO MORE than 'k' important contests, 
         * what is the MAXIMUM AMOUNT of LUCK 
         * she can have after competing in ALL the preliminary contests? 
         * 
         * THIS VALUE MAY BE NEGATIVE.
         */
    class Result
    {
        public Result(bool debug=false)
        {
            var input = new List<List<int>> { new List<int> { 5, 1}, new List<int> { 2, 1 },
                new List<int>{ 5, 1 }, new List<int>{ 5, 1 }, new List<int>{ 8, 1 }, new List<int>{ 10, 0 }, new List<int>{ 5, 0 }};

            Console.WriteLine(LuckBalance(3, input,debug));
        }

        public static int LuckBalance(int k, List<List<int>> contests,bool debug)
        {
            int totalContests = contests.Count;
            int luckSaved = 0;
            int maxImportantLuck = 0;

            Dictionary<int, int> importantMatchesLuckFreq = new Dictionary<int, int>(totalContests);
            foreach (var contest in contests)//O(N)
            {
                if (contest[1] == 0)
                {
                    luckSaved += contest[0];
                }
                else
                {
                    if (contest[0] > maxImportantLuck)
                    {
                        maxImportantLuck = contest[0];
                    }
                    if (!importantMatchesLuckFreq.ContainsKey(contest[0]))//O(1)
                    {
                        importantMatchesLuckFreq.Add(contest[0], 1);//O(1)
                    }
                    else
                    {
                        importantMatchesLuckFreq[contest[0]]++;
                    }
                }
            }
            Debug.PrintForDebug($"Max Important Luck {maxImportantLuck}",debug);
            Debug.PrintForDebugDictKvp(importantMatchesLuckFreq,true);
            Debug.PrintForDebug($"Saved Luck (From Unimportant) {luckSaved}", debug);
            int importantLosses = 0;
            //For all luck values starting from maximum luck value in contest dictionary
            for (int i = maxImportantLuck; i > 0; i--) //O(n)
            {
                if (importantLosses < k) //If important losses are less than acceptable important losses
                {   //if contest dictionary contains luck value
                    if (importantMatchesLuckFreq.ContainsKey(i) && importantMatchesLuckFreq[i] >= 1)//O(1)
                    {
                        //if the frequency of luck value + important losses so far <= acceptable important losses
                        if (importantLosses + importantMatchesLuckFreq[i] <= k)
                        {
                            luckSaved += i * importantMatchesLuckFreq[i];//increase saved luck by luck value X frequency of luck value
                            importantLosses += importantMatchesLuckFreq[i];//increase important losses by frequency of luck value
                            importantMatchesLuckFreq[i] = 0; //set frequency of luck value to zero
                            Debug.PrintForDebug($"{i} in boundaries {importantLosses} important losses", debug);
                        }
                        else//if the frequency of luck value + important losses so far > acceptable important losses
                        {
                            luckSaved += i * (k - importantLosses);//increase saved luck by luck value X amount to reach acceptable important losses
                            importantMatchesLuckFreq[i] -= k - importantLosses;//decrease frequency of luck value by that same amount
                            importantLosses = k; //update important losses to acceptable important losses
                            Debug.PrintForDebug($"Saved Luck {luckSaved}", debug);
                            //Margin case where freq>1 and important losses surpass k for some freq'<freq.
                            //The remainder frequency will count towards WINS
                            luckSaved -= i * importantMatchesLuckFreq[i];//decrease saved luck by updated frequency of luck value
                            importantMatchesLuckFreq[i] = 0; //set frequency of luck value to zero
                            Debug.PrintForDebug($"{i} out of boundaries {importantLosses} important losses", debug);
                        }

                    }
                    else  //if contest dictionary does not contain luck value
                    {
                        Debug.PrintForDebug($"{i} nonexistent", debug);
                        continue;//skip
                    }
                }
                else //If important losses are greater than acceptable important losses, do the same as above but 
                {    //DECREASE saved luck
                    if (importantMatchesLuckFreq.ContainsKey(i) && importantMatchesLuckFreq[i] >= 1)//O(1)
                    {
                        luckSaved -= i * importantMatchesLuckFreq[i];
                        importantMatchesLuckFreq[i] = 0;
                        Debug.PrintForDebug($"{i} irrelevant", debug);
                    }
                    else
                    {
                        Debug.PrintForDebug($"{i} nonexistent", debug);
                        continue;
                    }
                }

                Debug.PrintForDebug($"Saved Luck {luckSaved}",debug);
            }
            return luckSaved;
        }
    }
}
