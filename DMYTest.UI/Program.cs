
using System;

namespace DMYTest.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] flag;
            do
            {
                flag[0] = true;
                while (flag[1]){}
                // critical section
                flag[0] = false;    
                // remainder section
            } while (true);

            do
            {
                flag[1] = true;
                while (flag[0]) { }
                // critical section
                flag[1] = false;
                // remainder section
            } while (true);

        }
    }
}
