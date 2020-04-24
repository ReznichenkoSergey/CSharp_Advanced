using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace IteaThreads
{
    public enum MethodType
    {
        Simple,
        Semaphore,
        Locker
    }

    public class MyClass
    {
        private object locker = new object();
        
        private static int Counter { get; set; }

        private Semaphore Semaphore;

        public int Limit { get; private set; }

        public MyClass(int limit) 
        {
            Limit = limit;
        }

        private List<Thread> GetListThreads(Action action)
        {
            return new List<Thread>()
            {
                new Thread(new ThreadStart(action)){
                        Name = $"#1",
                Priority = ThreadPriority.BelowNormal
                },
                new Thread(new ThreadStart(action)){
                        Name = $"#2",
                Priority = ThreadPriority.Normal
                },
                new Thread(new ThreadStart(action)){
                        Name = $"#3",
                Priority = ThreadPriority.AboveNormal
                },
                new Thread(new ThreadStart(action)){
                        Name = $"#4",
                Priority = ThreadPriority.Highest
                },
                new Thread(new ThreadStart(action)){
                        Name = $"#5",
                Priority = ThreadPriority.Lowest
                }
            };
        }

        public void RunTest(MethodType methodType, int initCount=0, int maxCount=0)
        {
            Counter = 0;
            switch (methodType)
            {
                case MethodType.Simple:
                    GetListThreads(RunCounter)
                        .ForEach(x => x.Start());
                    break;
                case MethodType.Semaphore:
                    var list = GetListThreads(RunSemaphoreCounter);
                    Semaphore = new Semaphore(initCount, maxCount);
                    list
                        .ForEach(x => x.Start());
                    break;
                case MethodType.Locker:
                    GetListThreads(RunLockerCounter)
                .ForEach(x => x.Start());
                    break;
            }
        }

        private void RunCounter()
        {
            Thread thread = Thread.CurrentThread;
            for (int i = 0; i < Limit; i++)
            {
                Counter++;
                Console.WriteLine($"{thread.Name} ({thread.Priority}): Counter= {Counter}");
            }
        }

        private void RunSemaphoreCounter()
        {
            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"{thread.Name} ({thread.Priority}): is waiting");
            Semaphore.WaitOne();
            Console.WriteLine($"{thread.Name} ({thread.Priority}): passed");
            for (int i = 0; i < Limit; i++)
            {
                Counter++;
                Console.WriteLine($"{thread.Name} ({thread.Priority}): i= {i}, Counter= {Counter}");
            }
            Console.WriteLine($"{thread.Name} ({thread.Priority}): released");
            Semaphore.Release();
        }

        private void RunLockerCounter()
        {
            lock (locker)
            {
                Thread thread = Thread.CurrentThread;
                for (int i = 0; i < Limit; i++)
                {
                    Counter++;
                    Console.WriteLine($"{thread.Name} ({thread.Priority}): i= {i}, Counter= {Counter}");
                }
            }
        }
    }
}
