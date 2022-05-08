using System.Diagnostics;

namespace Lesson15_1
{
    class Program
    {
        public static string? Name { get; set; }
        static void Main()
        {
            ////////////////////////////////////
            //// Null Forgiving operator
            // Console.WriteLine(Name.Length);





            ////////////////////////////////////
            //// goto

            // int number = 2;
            // 
            // switch (number)
            // {
            //     case 0:
            //         Console.WriteLine("A");
            //         break;
            //     case 1:
            //         Console.WriteLine("B");
            //         break;
            //     case 2:
            //         Console.WriteLine("C");
            //         goto case 0;
            // }







            for (int i = 0; i < 10; i++)
            {
                if (i == 5) goto label;

                Console.WriteLine(i);
            }

            Console.WriteLine("DoSomehting");

            label:

            Console.WriteLine("Terminated");
        }
    }
}