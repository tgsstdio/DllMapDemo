using System;
using System.Runtime.InteropServices;

namespace NetStandardLib
{
    public class SimpleFib
    {
        const string LIBRARY_NAME = "OldLib";

        [DllImport(LIBRARY_NAME)]
        static extern void fibonacci_init(ulong i1, ulong i2);

        [DllImport(LIBRARY_NAME)]
        static extern bool fibonacci_next();

        [DllImport(LIBRARY_NAME)]
        static extern uint fibonacci_index();

        [DllImport(LIBRARY_NAME)]
        static extern ulong fibonacci_current();


        public static void Display(uint maxIterations)
        {
            // Initialize a Fibonacci relation sequence.
            fibonacci_init(1, 1);
            // Write out the sequence values until overflow.

            var i = 0;
            do
            {
                Console.WriteLine("{0}: {1}", fibonacci_index(), fibonacci_current());
                i += 1;
            } while (i < maxIterations && fibonacci_next());
        }

        [DllImport("NewLib")]
        static extern int NativeSum(int arg1, int arg2);

        public static int GetSum(int arg1, int arg2)
        {
            return NativeSum(arg1, arg2);
        }
    }
}
