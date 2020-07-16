using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XLVI_Dejan_Prodanovic
{
    class Vehicles
    {
        public delegate void PrintInfo(int numberOfVehicles, Dictionary<Thread, string> vehicles);
        //List<Thread> vehicles = new List<Thread>();
        Dictionary<Thread,string> vehicles = new Dictionary<Thread, string>();
        string[] directions = { "south","north"};
        Random rnd = new Random();

        public void GenerateVehicles()
        {
            int numberOfVehicles = rnd.Next(1,16);
            int directionIndex = rnd.Next(0,2);

            for (int i = 0; i < numberOfVehicles; i++)
            {
                string direction = directions[directionIndex];
                Thread thread = new Thread(() => CrossTheBridge(direction));
                thread.Name = String.Format("Vehicle{0}", i + 1);
                //vehicles.Add(thread);
                vehicles.Add(thread,direction);
                thread.Start();
            }
            foreach (KeyValuePair<Thread, string> veh in vehicles)
            {

                veh.Key.Join();
            }
        }

        public void CrossTheBridge(string direction)
        {
            Thread.Sleep(3000);
            string vehicleName = Thread.CurrentThread.Name;
            Console.WriteLine("{0} crossed the bridge", vehicleName);
        }
    }
}
