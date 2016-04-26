using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hit to start.");
            Console.ReadLine();

            var producer = new Producer();
            producer.Run();
        }
    }
}
