using System;
using System.Collections.Generic;
using System.Linq;
using IteaDelegates.IteaMessanger;
using static ITEA_Collections.Common.Extensions;

namespace IteaLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Comment

            //List<Person> people = GetPeople().ToList();

            //foreach (Person x in people)
            //{
            //    ToConsole(x.ToString(), ConsoleColor.Cyan);
            //}

            //people
            //    .ForEach(x => ToConsole(x.ToString(), ConsoleColor.Cyan));

            //people
            //    .CustomWhere(x => x.Age < 28)
            //    .ToList()
            //    .ForEach(x => ToConsole(x.ToString(), ConsoleColor.Cyan));

            //foreach (Person x in from i in people where i.Age < 28 select i)
            //    ToConsole(x.ToString(), ConsoleColor.Cyan);

            //IOrderedEnumerable<Person> ordered1 = people
            //    .Where(x => x.Age > 35)
            //    .OrderByDescending(x => x.Age);

            //var ordered2 = from i in people
            //               where i.Age > 35
            //               orderby i.Age descending
            //               select new { i.Name };


            //int min = people.Min(x => x.Age);
            //int max = people.Max(x => x.Age);
            //double avr = people.Average(x => x.Age);

            //var tenten = people.Skip(10).Take(10);

            //var anon = new
            //{
            //    Name = "Anon",
            //    Age = 21
            //};

            //var anon1 = new
            //{
            //    Name = "Anon",
            //    Age = "dwqd"
            //};

            //ToConsole(anon.Age.GetType().Name);
            //ToConsole(anon1.Age.GetType().Name);

            //List<Person> people = new List<Person>
            //{
            //    new Person("Pol", Gender.Man, 37, "pol@gmail.com"),
            //    new Person("Ann", Gender.Woman, 25, "ann@yahoo.com"),
            //    new Person("Alex", Gender.Man, 21, "alex@gmail.com"),
            //    new Person("Harry", Gender.Man, 58, "harry@yahoo.com"),
            //    new Person("Germiona", Gender.Woman, 18, "germiona@gmail.com"),
            //    new Person("Ron", Gender.Man, 24, "ron@yahoo.com"),
            //    new Person("Etc1", Gender.etc, 42, "etc1@yahoo.com"),
            //    new Person("Etc2", Gender.etc, 42, "etc2@gmail.com"),
            //};

            //people
            //    .CustomWhere(x => x.Email.Contains("gmail"))
            //    .ShowAll()
            //    .OrderByDescending(x => x.Age)
            //    .ShowAll();

            #endregion

            //Создание групп
            List<Group> listGroup = new List<Group>();
            listGroup.Add(new Group("Group #1"));
            listGroup.Add(new Group("Group #2"));
            
            //Создание аккаунтов
            List<Account> listAccount = new List<Account>();
            for (int i = 0; i < 5; i++)
            {
                Account account = new Account($"User_{i}");
                for (int j = 0; j < listGroup.Count; j++)
                {
                    account.Subscribe(listGroup[j], false);
                }
                listAccount.Add(account);
            }
            ToConsole("* Accounts were subscribed!\n", ConsoleColor.Cyan);

            //Отправка сообщений            
            Random random = new Random();
            Message message1 = null;
            for (int i = 0; i < listAccount.Count; i++)
            {
                int k = random.Next(3, 5);
                for (int j = 0; j < k; j++)
                {
                    Account account = listAccount[i];
                    //
                    message1 = account.CreateMessage($"Your value is {random.Next(100)}!!!!", listGroup[0]);
                    account.SendGroupMessage(message1, listGroup[0]);
                    //
                    message1 = account.CreateMessage($"Your value is {random.Next(100)}!!!!", listGroup[1]);
                    account.SendGroupMessage(message1, listGroup[1]);                    
                    //
                    if ((i + 1) < listAccount.Count)
                    {
                        message1 = account.CreateMessage($"Your value is {random.Next(100)}!!!!", listAccount[i + 1]);
                        account.Send(message1);
                    }
                }
            }


            //Вывод сообщений
            //Выведите количество сообщений между каждым из адресатов пользователя (ключ – имя пользователя/группы, значение – количество сообщений).
            ToConsole();
            ToConsole($"** Кол-во сообщений от {listAccount[random.Next(0, listAccount.Count-1)].Username} **", ConsoleColor.Yellow);
            listAccount[0].Messages
                .GroupBy(x => x.To.Username)
                .Select(y => new { Name = y.Key, Count = y.Count() })
                .ToList()
                .ForEach((x) => ToConsole($"{x.Name} - {x.Count}"));

            //найдите 3 самых активных пользователей
            ToConsole($"** 3 самых активных пользователя в группе {listGroup[0].Username} **", ConsoleColor.Yellow);
            Group group = listGroup[random.Next(0, listGroup.Count - 1)];
            group.Messages
                .GroupBy(x => x.From.Username)
                .Select(x => new { UserName = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .Take(3)
                .ToList()
                .ForEach((x) => ToConsole($"{x.UserName} - {x.Count}"));

            ToConsole();
            ToConsole("Укажите имя пользователя для вывода статистики сообщений:", ConsoleColor.Green);
            string userName = Console.ReadLine();
            if (!string.IsNullOrEmpty(userName))
            {
                group.Messages
                    .Where(x => x.From.Username.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
                    .OrderByDescending(x => x.Created)
                    .Select(x => new { Date = x.Created, Content = x.ReadMessage(x.From) })
                    .ToList()
                    .ForEach((x) => ToConsole($"{userName}: {x.Date} - {x.Content}"));
            }

            Console.ReadLine();

    }

    #region Create people list
    public static IEnumerable<Person> GetPeople()
        {
            for (int i = 0; i < 20; i++)
            {
                yield return new Person("Person" + i, 18 + i * 2);
            }
        }
        #endregion

        static void BaseDelegates(int f, int s)
        {
            Action<int, int> action = (a, b) => Console.WriteLine($"{a}{b}");
            Predicate<int> predicate = (a) => a > 0;
            Func<int, int, string> func = delegate (int a, int b)
            {
                return (a * b).ToString();
            };
            action(f, s);
            predicate(f);
            func(f, s);
        }

    }
}
