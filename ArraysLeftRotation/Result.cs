using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace ArraysLeftRotation
{
    /// <summary>
    /// Arrays: Left Rotation<br></br>
    /// Given an array of integers and a number, a, perform left rotations on the array. <br></br>
    /// Return the updated array <br></br>
    /// Constraints:<br></br>
    /// 1&lt;=n&lt;=10^5<br></br>
    /// 1&lt;=d&lt;=n<br></br>
    /// 1&lt;=a[i]&lt;=10^6<br></br>
    /// </summary>
    class Result
    {
        /*
         * Constraints:
         * 1<=n<=10^5
         * 1<=d<=n
         * 1<=a[i]<=10^6
        */

        public delegate List<int> RotLeftMethods(List<int> a, int shift);
        public Result(bool debug)
        {
            Debug.PrintForDebug("BruteForce", debug);
            TestCases(RotLeftBrute, debug);
            Debug.PrintForDebug("Optimal", debug);
            TestCases(RotLeft, debug);
            Debug.PrintForDebug("Optimal General", debug);
            TestCases(RotLeftOptimal, debug);
            
        }


        public static void TestCases(RotLeftMethods del, bool debug)
        {
            List<int> input = new List<int> { 1, 2, 3, 4, 5 };
            Debug.PrintIEnumerable(input, debug);

            for (int shift = 0; shift <= (del==RotLeft?input.Count:2*input.Count); shift++)
            {
                Debug.PrintForDebug($"Left Shift all by {shift}:", debug);
                
                var result = del(input, shift);
                Debug.PrintIEnumerable(result, debug);  
            }
            Debug.PrintForDebug("-------", debug);
        }



        //Brute Force: Time O(n^2) (will time out) ,Auxiliary space O(1).
        public static List<int> RotLeftBrute(List<int> a, int d)
        {           
            while (d!=0)
            {
                int temp = a[0];
                for (int i = 1; i < a.Count; i++)
                {
                    a[i - 1] = a[i];
                }
                a[a.Count - 1] = temp;
                d--;
            }
            return a;
        }


        //Consider a with length of n: {a[0], a[1], ...., a[j-1], a[j], a[j+1],....a[n-2],a[n-1]}
        //for each element a[j] we need to shift a[j] to a new position a[j-d] 
        //Let's consider the case where the shifts d are always less than or equal to n.
        //Then:
        //if n-(j-d) <= n : a[j]--> a[j-d] 
        //else              a[j]--> a[n-(d-j)]
        //example n=5, d=3
        //j=4, d=3 : 5 - (4-3) = 4 < =n  a[4]->a[4-d]=a[1]
        //j=3, d=3 : 5 - (3-3) = 5 < =n  a[3]->a[3-d]=a[0]
        //j=2, d=3 : 5 - (2-3) = 6 > =n  a[2]->a[n-(d-2)]=a[4] 
        //j=1, d=3 : 5 - (1-3) = 7 > =n  a[1]->a[n-(d-1)]=a[3]
        //j=0, d=3 : 5 - (0-3) = 8 > =n  a[0]->a[n-(d-0)]=a[2]
        //This algorithm will not work for d>n.


       //Time complexity O(n), Auxiliary space O(n)
        public static List<int> RotLeft(List<int> a, int d)
        {
            int n = a.Count;
            if (d>n || n<0)
            {
                throw new ArgumentOutOfRangeException("Left rotations cannot be zero or exceed array length");
            }
            
            List<int> aRot = new List<int>(n);
            for (int i = 0; i < n; i++)
            {
                aRot.Add(0);
            }

            for (int i = 0; i < n; i++)
            {
                if (n - (i - d) <= n)
                {
                    aRot[i - d] = a[i];
                }
                else
                {
                    aRot[n - (d - i)] = a[i];
                }
            }

            return aRot;
        }
        //Follow up: Could you do an O(n) time complexity solution that works
        //for an arbitrary number of left rotations?
        //Use mod operations.
        //Time Complexity O(n), auxiliary space O(1).
        public static List<int> RotLeftOptimal(List<int> a, int d)
        {

            int n = a.Count;
            List<int> aRot = new List<int>(n);
            int mod = d % n; // To get the starting point of rotated array 
            for (int i = 0; i < n; i++)
            {
                aRot.Add(0);
            }

            for (int i = 0; i < n; i++)
            {
                aRot[i] = a[(i + mod) % n]; //To capture the left rotation.
            }

            return aRot;
           
        }
        //Follow up: Could you then do this using auxiliary space O(1)? Could you maybe do it in place?
    }
}
