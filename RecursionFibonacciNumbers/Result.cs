using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursionFibonacciNumbers
{
    /// <summary>
    /// Recursion: Fibonacci Numbers<br></br>
    /// fibonacci(0) = 0 <br></br>
    /// fibonacci(1) = 1 <br></br>
    /// fibonacci(n) = fibonacci(n-1) +fibonacci(n-2) <br></br>
    /// Given n, return the n-th term of the fibonacci sequence <br></br>
    /// Constraints:<br></br>
    /// 0&lt;n&lt;30
    /// </summary>
    class Result
    {
        private delegate int FibonacciMethods(int n);
        public Result()
        {
            Console.WriteLine("Optimal");
            TestCases(Fibonacci);
            Console.WriteLine("Recursion and Memoization");
            TestCases(FibonacciRecursionMemoization);
            Console.WriteLine("Recursion ");
            TestCases(FibonacciRecursion);
            Console.WriteLine("Bonus (the true optimal): Closed Form");
            TestCases(FibonacciClosedForm);
        }

        private static void TestCases(FibonacciMethods del)
        {
            for (int i = 0; i <= 30; i++)
            {
                var result = del(i);
                Console.WriteLine($"Fibonacci {i}: {result}");
            }
            Console.WriteLine("-----");
        }
        //Recursion
        //Time complexity O(2^n), auxiliary space O(n) (depth of recursion tree)
        private static int FibonacciRecursion(int n)
        {

            if (n==0)
            {
                return 0;
            }
            else if (n==1)
            {
                return 1;
            }
            else
            {
               return FibonacciRecursion(n - 1) + FibonacciRecursion(n - 2);              
            }

            

        }
        //Recursion & Memoization
        //Time Complexity O(n), auxiliary space O(n).
        private static int FibonacciRecursionMemoization(int n)
        {

            int[] fibos = new int[n + 2];
            fibos[0] = 0;
            fibos[1] = 1;
            return FiboMemo(n, fibos);

        }

        private static int FiboMemo(int n, int[] fibos)
        {
            if (fibos[n] == 0 && n > 1)
            {
                fibos[n] = FiboMemo(n - 1, fibos) + FiboMemo(n - 2, fibos);
            }
            return fibos[n];
        }
        //Optimal: Time Complexity O(n), Auxiliary space O(1)
        private static int Fibonacci(int n)
        {

            int fibo0 = 0;
            int fibo1 = 1;
            int temp;
            for (int i = 1; i <= n; i++)
            {
                temp = fibo1;
                fibo1 = temp + fibo0;
                fibo0 = temp;
            }
            return fibo0;

        }

        //Note: There is : a) explicit mathematical formula  and b) matrix multiplication method
        //for fibonacci that evaluate this problem in O(log(n)) time

        //Closed form Math expression related to the golden ratio φ
        //https://en.wikipedia.org/wiki/Fibonacci_number
        //Time Complexity O(log(n)) due to .Sqrt() && .Pow(),
        //Auxiliary space O(1)
        private static int FibonacciClosedForm(int n)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            return (int)Math.Round(Math.Pow(phi, n) / Math.Sqrt(5));
        }


    }
}
