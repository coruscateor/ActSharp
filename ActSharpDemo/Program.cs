using System;
using System.Diagnostics;
using System.Collections.Generic;
using ActSharp;
using ActSharp.Async;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ActSharpDemo
{

    class Program
    {

        /// <summary>
        /// A basic, yet chatty demonstration of Actsharp. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            HappyActor happyActor = new HappyActor();

            Stopwatch TotalSw = new Stopwatch();

            Console.WriteLine();

            Console.WriteLine("Main thread id:");

            WriteThreadId();

            int hwNo = 0;

            TotalSw.Start();

            do
            {

                var hwResult = happyActor.HelloWorld();

                Console.WriteLine("happyActor.HelloWorld():");

                Console.WriteLine();

                Console.WriteLine("hwResult.IsCompleted: " + hwResult.IsCompleted);
                
                var helloWorldResult = hwResult.Result;

                TotalSw.Stop();

                Console.WriteLine();

                Console.WriteLine(helloWorldResult);

                Console.WriteLine();

                Console.WriteLine("Call - Milliseconds: " + TotalSw.ElapsedMilliseconds + " - Ticks: " + TotalSw.ElapsedTicks); // + " - ActorManagedThreadId:" + happyActor.ActorManagedThreadId);

                Console.WriteLine();

                TotalSw.Reset();

                hwNo++;

            }
            while (hwNo < 20);

            Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("happyActor.Add():");

            Console.WriteLine();

            List<Task<int>> results = new List<Task<int>>(100);

            TotalSw.Reset();

            for (int i = 1; i < 101; i++)
            {

                TotalSw.Start();

                results.Add(happyActor.Add());

                TotalSw.Stop();

                Console.WriteLine("Call: " + i + " - Milliseconds: " + TotalSw.ElapsedMilliseconds + " - Ticks: " + TotalSw.ElapsedTicks); // + " - ActorManagedThreadId:" + happyActor.ActorManagedThreadId);

                TotalSw.Reset();

            }

            Console.WriteLine();

            Console.WriteLine("happyActor.Add() Results:");

            Console.WriteLine();

            foreach (var item in results)
            {

                Console.WriteLine(item.IsCompleted.ToString() + " " + item.Result.ToString());

            }

            Console.WriteLine();

            Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("happyActor.Add() - Multi-Threads:");

            Console.WriteLine();

            List<Task> taskResults = new List<Task>(80); 

            Action<int> hActorAddAction = (int i) => {

                Console.WriteLine();

                Console.WriteLine("hActorAddAction thread id:");

                WriteThreadId();

                Stopwatch sw = new Stopwatch();

                sw.Start();

                var at = happyActor.Add();

                sw.Stop();

                Console.WriteLine("Call: " + i + " - Milliseconds: " + sw.ElapsedMilliseconds + " - Ticks: " + sw.ElapsedTicks);

                Console.WriteLine("Call: " + i + " - Task.IsCompleted: " + at.IsCompleted);

                Console.WriteLine("Call: " + i + " - Task.Result: " + at.Result);

            }; 

            TotalSw.Reset();

            for (int i = 1; i < 81; i++)
            {

                taskResults.Add(hActorAddAction.Async(i));

            }

            Console.WriteLine();

            taskResults.WaitAll();

            Console.ReadLine();

            happyActor.Dispose();

        }

        static void WriteThreadId()
        {

            Console.WriteLine();

            Console.WriteLine("Thread Id: " + Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine();

        }

    }

}
