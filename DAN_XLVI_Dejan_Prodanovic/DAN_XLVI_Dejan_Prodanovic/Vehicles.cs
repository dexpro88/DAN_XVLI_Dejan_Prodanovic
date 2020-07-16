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
       
        private SemaphoreSlim northSemaphore = new SemaphoreSlim(2,5);
        private SemaphoreSlim southSemaphore = new SemaphoreSlim(0, 5);
        public BackgroundWorker worker = new BackgroundWorker();
        int counter;
        
        bool northSemaphoreON = true;
        bool southSemaphoreON = false;
      
        public delegate void PrintInfo(Dictionary<Thread, string> dictionary);

        //dictionary that stores threads(that represent vehicles) and their directions
        Dictionary<Thread,string> vehicles = new Dictionary<Thread, string>();
        string[] directions = { "south","north"};
        Random rnd = new Random();

        public Vehicles()
        {
            worker.DoWork += DoWork;
        }

        /// <summary>
        /// method that generates random number of threads that represent vehicles
        /// threads are parametrized  with CrossTheBridge method that gets random direction
        /// ad parametar
        /// </summary>
        /// <param name="printInfo"></param>
        public void GenerateVehicles(PrintInfo printInfo)
        {
            int numberOfVehicles = rnd.Next(1, 16);
            //int numberOfVehicles = 120;
            counter = numberOfVehicles;

            for (int i = 0; i < numberOfVehicles; i++)
            {
                int directionIndex = rnd.Next(0, 2);
                string direction = directions[directionIndex];
                Thread thread = new Thread(() => CrossTheBridge(direction));
                thread.Name = String.Format("Vehicle{0}", i + 1);
               
                vehicles.Add(thread,direction);              
            }
            //we start background worker that will will be changing semaphor state on every 500 ms
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

        /// <summary>
        /// method that represent crossing of bridge by vehicles
        /// </summary>
        /// <param name="direction"></param>
        public void CrossTheBridge(string direction)
        {
            Console.WriteLine("{0} waits on the semaphore ",Thread.CurrentThread.Name);


            if (direction.Equals("north"))
            {
                //if direction parametar is "north" we use northSemaphore
                northSemaphore.Wait();
                    
               Console.WriteLine("{0} crosses the bridge. It goes north", Thread.CurrentThread.Name);

               Thread.Sleep(500);
               counter--;
                //northSemaphore is released if northSemaphoreON is true 
                if (northSemaphoreON && northSemaphore.CurrentCount<2)
                {
                    northSemaphore.Release();

                }

            } else if(direction.Equals("south"))
            {
                //if the direction parametar is "south" we use southSemaphore 
                southSemaphore.Wait();

                Console.WriteLine("{0} crosses the bridge. It goes south", Thread.CurrentThread.Name);

                Thread.Sleep(500);
                counter--;
                if (southSemaphoreON && southSemaphore.CurrentCount<2)
                {
                    southSemaphore.Release();
                }
            }           
            
        }
        /// <summary>
        /// DoWork method of background worker
        /// it alternately changes state of southSemaphoreON and northSemaphoreON
        /// and it alternately relases southSemaphore and northSemaphore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DoWork(object sender, DoWorkEventArgs e)
        {
            while (counter!=0)
            {
                
                if (northSemaphoreON)
                {
                    northSemaphoreON = false;
                    
                    southSemaphoreON = true;
                   
                    if (southSemaphore.CurrentCount==0)
                    {
                        southSemaphore.Release(2);
                    }

                }
                else
                {
                    southSemaphoreON = false;
                    
                    northSemaphoreON = true;
                    if (northSemaphore.CurrentCount==0)
                    {
                        northSemaphore.Release(2);
                    }
                   
                }
                Thread.Sleep(500);
            }
           
        }


    }
}
