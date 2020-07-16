using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XLVI_Dejan_Prodanovic
{
    class Vehicles
    {
       
        private SemaphoreSlim northSemaphore = new SemaphoreSlim(2, 2);
        private SemaphoreSlim southSemaphore = new SemaphoreSlim(0, 2);
        public BackgroundWorker worker = new BackgroundWorker();
        public Vehicles()
        {
            worker.DoWork += DoWork;
        }
        bool northSemaphoreON = true;
        bool southSemaphoreON = false;

        public delegate void PrintInfo(Dictionary<Thread, string> dictionary);
        //List<Thread> vehicles = new List<Thread>();
        Dictionary<Thread,string> vehicles = new Dictionary<Thread, string>();
        string[] directions = { "south","north"};
        Random rnd = new Random();

        public void GenerateVehicles(PrintInfo printInfo)
        {
            //int numberOfVehicles = rnd.Next(1,16);
            int numberOfVehicles = 40;

            for (int i = 0; i < numberOfVehicles; i++)
            {
                int directionIndex = rnd.Next(0, 2);
                string direction = directions[directionIndex];
                Thread thread = new Thread(() => CrossTheBridge(direction));
                thread.Name = String.Format("Vehicle{0}", i + 1);
               
                vehicles.Add(thread,direction);              
            }
            worker.RunWorkerAsync();
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
            Console.WriteLine("{0} waits on the semaphore ",Thread.CurrentThread.Name);

            if (direction.Equals("north"))
            {
                northSemaphore.Wait();
                    
                    Console.WriteLine("{0} crosses the bridge. It goes north", Thread.CurrentThread.Name);

                    Thread.Sleep(2000);

                    northSemaphore.Release();

                    //Console.WriteLine("Thread {0} releases the semaphore.", num);
                    //Console.WriteLine("Thread {0} previous semaphore count: {1}",
                    //    num, _pool.Release());
                    //Console.WriteLine("Nesto moje: {0}", _pool.CurrentCount);
                
            }                 
            //Thread.Sleep(3000);
            //string vehicleName = Thread.CurrentThread.Name;
            //Console.WriteLine("{0} crossed the bridge", vehicleName);
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                //Console.WriteLine("\n\n\nradim nesto\n\n\n");
                if (northSemaphoreON)
                {
                    northSemaphoreON = false;
                    Thread.Sleep(200);
                    southSemaphoreON = true;
                }
                else
                {
                    southSemaphoreON = false;
                    Thread.Sleep(200);
                    northSemaphoreON = true;
                }
                Thread.Sleep(1100);
            }
           
        }


    }
}
