using System;

namespace Lesson_1
{
    class Program
    {
        static void Main(string[] args)
        {

            var anon = new
            {
                Name = "Anon",
                Age = 21
            };

            var anon1 = new
            {
                Name = "Anon",
                Age = "dwqd"
            };

            Console.WriteLine(anon.GetType().Name);
            Console.WriteLine(anon1.GetType().Name);
            Console.WriteLine("Hello World!");
        }
    }
}
