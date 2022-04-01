using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonChild
{
    /// <summary>
    /// A string is said to be a child of a another string if it can be formed by deleting 0 or more characters<br></br>
    /// from the other string. Letters cannot be rearranged. Given two strings of equal length, <br></br>
    /// what's the longest string that can be constructed such that it is a child of both? <br></br>
    /// Constraints:<br></br>
    /// 1&lt;= s1.length, s2.length &lt;=5000<br></br>
    /// all characters are uppercase in the range ascii[A-Z]<br></br>
    /// </summary>
    class Result
    {
        //Examples: ABCD ABDC highest common child ABC or ABD length=3
        //HARRY SALLY highest common child AY length=2
        //AA BB no common child length=0
        //SHINCHAN NOHARAA highest common child, NHA length=3

        //*Longest common subsequence problem.
        //https://en.wikipedia.org/wiki/Longest_common_subsequence_problem 
        //No way I could not have solved this alone.

        public static int CommonChild(string s1, string s2)
        {          
            int m = s1.Length;
            int n = s2.Length;
            int[,] T = new int[m+1, n+1];

            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        T[i,j] = 0;
                    }
                    else if (s1[i - 1] == s2[j - 1])
                    {
                        T[i,j] = 1 + T[i - 1,j - 1];
                    }
                    else
                    {
                        T[i,j] = Math.Max(T[i,j - 1], T[i - 1,j]);
                    }
                }
            }
            return T[m,n];         
        }
    }
}
