using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XLVI_Dejan_Prodanovic
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicles veh = new Vehicles();
            veh.GenerateVehicles(PrintVehiclesInfo);
            Console.WriteLine("All vehicles crossed the bridge");
            Console.ReadLine();
        }

        public static void PrintVehiclesInfo(Dictionary<Thread, string> vehicles)
        {
            Console.WriteLine("Total number of vehicles {0}", vehicles.Count);
            Console.WriteLine("\nList of vehicles and their directions");
            Console.WriteLine("----------------------------------------");
            foreach (KeyValuePair<Thread, string> vehicle in vehicles)
            {
                Console.WriteLine("{0} {1}",
               vehicle.Key.Name, vehicle.Value);
            }
        }
    }
}
