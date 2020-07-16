using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            EventClass endEvent = new EventClass();
            EndProgramNotification endProgramNotification = new EndProgramNotification();
            endEvent.ProgramEnded += endProgramNotification.OnProgramEnded;
            endEvent.stopWatch.Start();
            veh.GenerateVehicles(PrintVehiclesInfo);
            
            Console.WriteLine("\nAll vehicles crossed the bridge\n");

            endEvent.stopWatch.Stop();
            endEvent.OnProgramEnded(endEvent.stopWatch.ElapsedMilliseconds);
            Console.ReadLine();
        }
        /// <summary>
        /// method that prints informations about vehicles and directions
        /// </summary>
        /// <param name="vehicles"></param>
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
