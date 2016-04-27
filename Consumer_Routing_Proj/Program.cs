using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer_Routing_Proj
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumer = new Consumer();
            consumer.Run(args);
        }
    }
}
