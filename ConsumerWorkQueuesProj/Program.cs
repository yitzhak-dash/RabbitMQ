using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerWorkQueuesProj
{
    class Program
    {
        static void Main(string[] args)
        {
            var cons = new Consumer();
            cons.Run();
            Console.ReadLine();
        }
    }
}
