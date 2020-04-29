using System;
using System.Reflection;
using StrategyGame.Warriors.Models.Infantry;

namespace IteaReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var tInt = 1.GetType();

            Console.WriteLine(tInt.Name);
            Console.WriteLine(tInt.FullName);

            foreach (var mInfo in tInt.GetMembers())
                Console.WriteLine($"{mInfo.Name}, {mInfo.DeclaringType}, {mInfo.MemberType}");


            Console.WriteLine("----------------------------------------");

            foreach (var mInfo in tInt.GetMethods())
                Console.WriteLine($"{mInfo.Name}, {mInfo.IsStatic}, {mInfo.IsConstructor}");

            Console.WriteLine("----------------------------------------");

            Bowman bowmanObj = new Bowman();
            Type bowman = bowmanObj.GetType();

            foreach (var mInfo in bowman.GetMembers())
                Console.WriteLine($"{mInfo.Name}, {mInfo.DeclaringType}, {mInfo.MemberType}");

            Console.WriteLine("----------------------------------------");

            foreach (var mInfo in bowman.GetMethods(BindingFlags.DeclaredOnly
            | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                Console.WriteLine($"{mInfo.Name}, {mInfo.IsStatic}, {mInfo.IsConstructor}");

        }
    }
}
