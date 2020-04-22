using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using IteaLinq;

using IteaLinqToSql.Models.Entities;

using Newtonsoft.Json;

namespace IteaAsync
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static List<int> ints = new List<int>();
        static List<string> strings = new List<string>();
        static async Task Main(string[] args)
        {
            Task<string> allUsersString = GetUserAsync();
            int count = 1;
            Task<User>[] taskArray = Enumerable.Repeat(0, 3).Select(x => GetUserByIdAsync(count++)).ToArray();

            Enumerable
                .Repeat(0, 1000)
                .Select(x => new Random().Next(1, 100))
                .ToList()
                .ForEach(x => Console.WriteLine($"Here we have {x}"));

            User[] users = await Task.WhenAll(taskArray);

            users.ToList().ForEach(x => Console.WriteLine(x?.ToString()));
            string allUsersResult = await allUsersString;

            List<Person> people = new List<Person>
            {
                new Person("Pol", Gender.Man, 37, "pol@gmail.com"),
                new Person("Ann", Gender.Woman, 25, "ann@yahoo.com"),
                new Person("Alex", Gender.Man, 21, "alex@gmail.com"),
                new Person("Harry", Gender.Man, 58, "harry@yahoo.com"),
                new Person("Germiona", Gender.Woman, 18, "germiona@gmail.com"),
                new Person("Ron", Gender.Man, 24, "ron@yahoo.com"),
                new Person("Etc1", Gender.etc, 42, "etc1@yahoo.com"),
                new Person("Etc2", Gender.etc, 42, "etc2@gmail.com"),
            };

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Func<Task> action = async () =>
            {
                await Task.Delay(2500);
                cancelTokenSource.Cancel();
            };

            Task toConsole = WriteToConsole(token);

            Task cancel = action();

            await cancel;
            try
            {
                await toConsole;
            }
            catch (OperationCanceledException) { }
            Console.WriteLine(toConsole.IsCompletedSuccessfully);

            Console.WriteLine("The End!");


            //Action<string> action = delegate (string item) 
            //{
            //    if (int.TryParse(item, out int i)) ints.Add(i);
            //    else strings.Add(item);
            //};

            //ReadFromFile("data.txt").ForEach((item) =>
            //{
            //    if (int.TryParse(item, out int i)) ints.Add(i);
            //    else strings.Add(item);
            //});

            //ints.ToFile("ints.txt");
            //strings.ToFile("string.txt");
        }

        public static void Ac(string item)
        {
            if (int.TryParse(item, out int i)) ints.Add(i);
            else strings.Add(item);
        }

        public static async Task<string> GetUserAsync()
        {
            try
            {
                var usersAsync = await client.GetAsync("http://localhost:5000/api/user");
                Console.WriteLine($"\nget usersAsync...\n");
                return await usersAsync.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}\n");
                return "";
            }
        }

        public static async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                await Task.Delay(id * 500);
                var usersAsync = await client.GetAsync($"http://localhost:5000/api/user/{id}");
                Console.WriteLine($"\nget userByIdAsync({id})...\n");
                string userStr = await usersAsync.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(userStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}\n");
                return null;
            }
        }

        public static async Task SimpleAsync(int number)
        {
            await Task.Delay(1000);
            Console.WriteLine(number);
        }

        public static async Task WriteToConsole(CancellationToken token)
        {

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                // if (token.IsCancellationRequested) return;
                token.ThrowIfCancellationRequested();
                Console.WriteLine($"WriteToConsole: {i + 1}sec");
            }
            Console.WriteLine("END!");
        }

        public static List<string> ReadFromFile(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                return streamReader.ReadToEnd().Split(' ').ToList();
            }
        }


        public static void InitListFromFile<T>(List<T> list)
        {
            list = new List<T>();
        }

        public static void Double(ref int a)
        {
            a *= 2;
        }
    }
}
