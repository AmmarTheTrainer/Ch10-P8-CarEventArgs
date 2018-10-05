using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10_P8_CarEventArgs
{
    class Program
    {
        static void Main(string[] args)
        {
            Car c1 = new Car("SlugBug", 100, 10);

            // Register event handlers.
            c1.AboutToBlow += C1_AboutToBlow1; ;


            c1.Exploded += (sender, e) => { Console.WriteLine(e.msg); };

            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);

            //EventArgs

            Console.ReadLine();
        }

        private static void C1_AboutToBlow1(object sender, CarEventArgs e)
        {
            if (sender is Car c)
            {
                Console.WriteLine();
                Console.WriteLine("Critical Message from {0}: {1}", c.PetName, e.msg);
            }
           // throw new NotImplementedException();
        }

        //private static void C1_AboutToBlow(string msgForCaller)
        //{
        //    Console.WriteLine(msgForCaller);
        //}
    }
}
