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
        int counter;
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
            counter = numberOfVehicles;

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
                    counter--;
                    //northSemaphore.Release();

                  
                    
                
            } else if(direction.Equals("south"))
            {
                southSemaphore.Wait();

                Console.WriteLine("{0} crosses the bridge. It goes south", Thread.CurrentThread.Name);

                Thread.Sleep(2000);
                counter--;
                //northSemaphore.Release();
            }           
            
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            while (counter!=0)
            {
                //Console.WriteLine("\n\n\nradim nesto\n\n\n");
                if (northSemaphoreON)
                {
                    northSemaphoreON = false;
                    //Thread.Sleep(100);
                    southSemaphoreON = true;
                   
                    if (southSemaphore.CurrentCount==0)
                    {
                        southSemaphore.Release(2);
                    }

                }
                else
                {
                    southSemaphoreON = false;
                    //Thread.Sleep(500);
                    northSemaphoreON = true;
                    if (northSemaphore.CurrentCount==0)
                    {
                        northSemaphore.Release(2);
                    }
                   
                }
                Thread.Sleep(2100);
            }
           
        }


    }
}
