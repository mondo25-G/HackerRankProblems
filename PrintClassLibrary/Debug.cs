using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintClassLibrary
{

    public static class Debug
    {
        public static void PrintAlphabetCounters(int[] counter, bool lowerCase, bool debug)
        {
            if (debug && counter.Length == 26)
            {
                for (int i = 0; i < 26; i++)
                {
                    Debug.PrintForDebug($"{Convert.ToChar(i + (lowerCase ? 97 : 65))}: {counter[i]}", debug);
                }
            }

        }
        public static void PrintForDebug(string message, bool debug)
        {
            if (debug)
            {
                Console.WriteLine(message);
            }

        }


        public static void PrintForDebug<T>(IList<T> iList, bool debug)
        {
            if (debug)
            {
                for (int i = 0; i < iList.Count; i++)
                {
                    Console.WriteLine($"{i}: {iList[i]}");
                }
            }
        }
        public static void PrintForDebugDictKvp<T, U>(IDictionary<T, U> dict, bool debug)
        {
            if (debug)
            {
                foreach (var item in dict)
                {
                    Console.WriteLine(item);
                }
            }

        }


        public static void PrintForDebugDictKvp<T, U>(IDictionary<T, IList<U>> dict, bool debug)
        {
            if (debug)
            {
                foreach (var item in dict)
                {
                    Console.Write($"{item.Key} : ");
                    PrintIEnumerable(item.Value, debug);
                }
            }

        }




        public static void PrintForDebugDictKeyValColl<T, U>(IDictionary<T, IList<U>> dict, bool debug)
        {
            if (debug)
            {
                Console.WriteLine("KEYS");
                foreach (var key in dict.Keys)
                {
                    Console.WriteLine(key);
                }
                Console.WriteLine("VALUES");
                foreach (var iList in dict.Values)
                {
                    Console.WriteLine(string.Join(",", iList));
                }
            }

        }


        public static void PrintIEnumerable<T>(IEnumerable<T> collection, bool debug)
        {
            if (debug)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in collection)
                {
                    sb.Append(item);
                    sb.Append(' ');
                }
                Console.WriteLine(sb.ToString());
            }

        }


    }
}
