using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLVI_Dejan_Prodanovic
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicles veh = new Vehicles();
            veh.GenerateVehicles();
            Console.WriteLine("All vehicles crossed the bridge");
            Console.ReadLine();
        }
    }
}
