
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            SyncMakeBreakfast();
            watch.Stop();
            Console.WriteLine("Sync Method Elapsed time: " + watch.ElapsedMilliseconds / 1000.0);
            
            watch.Restart();
            await AsyncMakeBreakfast_Method1();
            watch.Stop();
            Console.WriteLine("Async Method 1 Elapsed time: " + watch.ElapsedMilliseconds / 1000.0);

            watch.Restart();
            await AsyncMakeBreakfast_Method2();
            watch.Stop();
            Console.WriteLine("Async Method 2 Elapsed time: " + watch.ElapsedMilliseconds/1000.0);
        }

        static void SyncMakeBreakfast()
        {
            Coffee cup = new Coffee();
            cup.PourCoffee();
            
            Egg eggs = new Egg();
            eggs.EggMake();

            Bacon bacon = new Bacon();
            bacon.BaconFry();

            Toast toast = new Toast();
            toast.ToastBread();

            Juice juice = new Juice();
            juice.PourJuice();
        }

        static async Task AsyncMakeBreakfast_Method1()
        {
            Coffee cup = new Coffee();
            cup.PourCoffee();

            Egg eggs = new Egg();
            var eggsTask = eggs.AsyncEggMake();

            Bacon bacon = new Bacon();
            var baconTask = bacon.AsyncBaconFry();

            Toast toast = new Toast();
            var toastTask = toast.AsyncToastBread();

            await toastTask;
            Console.WriteLine("Toast ready!");

            await eggsTask;
            Console.WriteLine("Egg ready!");

            await baconTask;
            Console.WriteLine("Bacon ready!");

            Juice juice = new Juice();
            juice.PourJuice();
        }

        static async Task AsyncMakeBreakfast_Method2()
        {
            Coffee cup = new Coffee();
            cup.PourCoffee();

            Egg eggs = new Egg();
            var eggsTask = eggs.AsyncEggMake();

            Bacon bacon = new Bacon();
            var baconTask = bacon.AsyncBaconFry();

            Toast toast = new Toast();
            var toastTask = toast.AsyncToastBread();

            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                    Console.WriteLine("Egg ready!");
                else if(finishedTask == baconTask)
                    Console.WriteLine("Bacon ready!");
                else if(finishedTask == toastTask)
                    Console.WriteLine("Toast ready!");

                breakfastTasks.Remove(finishedTask);
            }

            Juice juice = new Juice();
            juice.PourJuice();
        }
    }

    class Coffee
    {
        public Coffee()
        { 
            Console.WriteLine("Coffee start");
        }

        public void PourCoffee()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Coffee Ready!");
        }
    }

    class Egg
    {
        public Egg()
        {
            Console.WriteLine("Egg start");
        }

        public void EggMake()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Egg ready!");
        }

        public async Task AsyncEggMake()
        {
            await Task.Delay(2000);
            
        }
    }

    class Bacon
    {
        public Bacon()
        {
            Console.WriteLine("Bacon start");
        }

        public void BaconFry()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Bacon ready!");
        }
        public async Task AsyncBaconFry()
        {
            await Task.Delay(3000);
        }

    }

    class Toast
    {
        public Toast()
        {
            Console.WriteLine("Toast start");
        }

        public void ToastBread()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Toast ready!");
        }
        public async Task AsyncToastBread()
        {
            await Task.Delay(1000);
        }
    }

    class Juice
    {
        public Juice()
        {
            Console.WriteLine("Juice start");
        }

        public void PourJuice()
        {
            Thread.Sleep(500);
            Console.WriteLine("Juice ready!");
        }
    }

}