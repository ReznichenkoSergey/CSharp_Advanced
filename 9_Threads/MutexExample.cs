using System;
using System.Threading;

namespace IteaThreads
{
    public class MutexExample
    {
        public class SharedRes
        {
            public static int count;
            public static Mutex mtx = new Mutex();
        }

        public class IncThread
        {
            int num;
            public Thread thread;

            public IncThread(string name, int n)
            {
                thread = new Thread(this.Run);
                num = n;
                thread.Name = name;
                thread.Start();
            }

            void Run()
            {
                Console.WriteLine(thread.Name + " ожидает мьютекс");
                SharedRes.mtx.WaitOne();

                Console.WriteLine(thread.Name + " получает мьютекс");

                do
                {
                    Thread.Sleep(500);
                    SharedRes.count++;
                    Console.WriteLine("в потоке {0}, Count={1}", thread.Name, SharedRes.count);
                    num--;
                } while (num > 0);

                Console.WriteLine(thread.Name + " освобождает мьютекс");

                SharedRes.mtx.ReleaseMutex();
            }
        }

        public class DecThread
        {
            int num;
            public Thread thread;

            public DecThread(string name, int n)
            {
                thread = new Thread(new ThreadStart(this.Run));
                num = n;
                thread.Name = name;
                thread.Start();
            }

            void Run()
            {
                Console.WriteLine(thread.Name + " ожидает мьютекс");

                SharedRes.mtx.WaitOne();

                Console.WriteLine(thread.Name + " получает мьютекс");

                do
                {
                    Thread.Sleep(500);
                    SharedRes.count--;
                    Console.WriteLine("в потоке {0}, Count={1}", thread.Name, SharedRes.count);
                    num--;
                } while (num > 0);

                Console.WriteLine(thread.Name + " освобождает мьютекс");

                SharedRes.mtx.ReleaseMutex();
            }
        }

    }
}
