using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerWorkQueuesProj
{
    class Program
    {
        static void Main(string[] args)
        {
            var pro = new Producer();
            pro.Run(args);
        }
    }
}
