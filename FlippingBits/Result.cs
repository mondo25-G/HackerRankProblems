using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintClassLibrary;

namespace FlippingBits
{
    /*
     * Given a long integer, write it down in 32-bit form, flip it (1->0, 0->1)
     * and return the new flipped integer
     * Constraints:
     * O<=n<=2^32
     */
    class Result
    {
        public Result(bool debug = false)
        {
            Console.WriteLine("Reversed "+FlippingBits(2147483647, debug) +"\n");
            Console.WriteLine("Reversed "+FlippingBits(1,debug) + "\n");
            Console.WriteLine("Reversed "+FlippingBits(0,debug) + "\n");
        }
        private static long FlippingBits(long n, bool debug)
        {
            Console.WriteLine("Initial: "+n);
            //ensure binary has length of 32 digits each digit initialized at 0.
            long[] binary = new long[32];
            //flipped will involve POWERS of 2 therefore DOUBLE.
            double flipped = 0;
            //This is a varying length to fill binary with 1s and 0
            //from RIGHT to LEFT.
            long varyLength = 32;
            //Algorithm to write n in binary form.
            while (n > 0)
            {
                //Fill in Remainder of division of integer by 2 from RIGHT to LEFT.
                binary[varyLength - 1] = (n % 2);
                //Move index to the LEFT
                varyLength--;
                //Reset integer to quotient
                n = n / 2;
                //Repeat until n=0
            }
            //DEBUG
            if (debug)
            {
                string testStr = null;
                for (int i = 0; i < binary.Length; i++)
                {
                    testStr += binary[i];
                }
                Console.WriteLine("Binary "+testStr);
            }          
            //DEBUG
            for (int i = 0; i < 32; i++)
            {
                if (binary[i] == 0) //This is where WE FLIP binary.
                                    //i.e. if digit =1 IGNORE IT because after FLIP it will be 0
                                    //and will not be summed. Take into account only digit=0
                                    // and treat them as if digit=1 in next step.
                {
                    flipped += Math.Pow(2, 31 - i); //And calculate FLIPPED INTEGER.
                                                    //IMPORTANT: digit at position i corresponds to integer 2^{31-i)
                                                    //NOT 2^{i} !!!

                }
            }
            return (long)flipped;
        }
    }
}
