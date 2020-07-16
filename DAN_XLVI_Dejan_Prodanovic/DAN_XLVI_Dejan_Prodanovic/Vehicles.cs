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
        private SemaphoreSlim semaphore = new SemaphoreSlim(0, 3);
        public delegate void PrintInfo(Dictionary<Thread, string> dictionary);
        //List<Thread> vehicles = new List<Thread>();
        Dictionary<Thread,string> vehicles = new Dictionary<Thread, string>();
        string[] directions = { "south","north"};
        Random rnd = new Random();

        public void GenerateVehicles(PrintInfo printInfo)
        {
            int numberOfVehicles = rnd.Next(1,16);
           

            for (int i = 0; i < numberOfVehicles; i++)
            {
                int directionIndex = rnd.Next(0, 2);
                string direction = directions[directionIndex];
                Thread thread = new Thread(() => CrossTheBridge(direction));
                thread.Name = String.Format("Vehicle{0}", i + 1);
               
                vehicles.Add(thread,direction);              
            }

            printInfo(vehicles);

            foreach (KeyValuePair<Thread, string> veh in vehicles)
            {
                veh.Key.Start();
                
            }
            foreach (KeyValuePair<Thread, string> veh in vehicles)
            {              
                veh.Key.Join();
            }
        }

        public void CrossTheBridge(string direction)
        {
            Console.WriteLine("{0} waits on the semaphore to cross the bridge ",Thread.CurrentThread.Name);
               
            semaphore.Wait();
            //Thread.Sleep(3000);
            //string vehicleName = Thread.CurrentThread.Name;
            //Console.WriteLine("{0} crossed the bridge", vehicleName);
        }

       
    }
}
