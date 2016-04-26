using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer_ReceiveLog_Proj
{
    class Program
    {
        static void Main(string[] args)
        {
            var cons = new Consumer();
            cons.Run();
        }
    }
}
