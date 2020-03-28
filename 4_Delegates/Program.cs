using System;
using ITEA_Collections;
using IteaDelegates.IteaMessanger;

namespace IteaDelegates
{
    public delegate double MathOperation(double a, double b);
    public delegate void PersonUpdate(Person person);

    class Program
    {
        static void Main(string[] args)
        {
            #region Test
            //Account a1 = new Account("Harry");
            //Account a2 = new Account("Ron");
            //SpyNotifications spy = new SpyNotifications(a2);
            //a1.Send(a1.CreateMessage("Hi, Ann! How are you?", a2));
            //a1.Send(a1.CreateMessage("I'm Alex, from Kyiv", a2));
            //a2.Send(a2.CreateMessage("Hi, Alex! I'm from Lviv", a1));
            //a1.ShowDialog(a2.Username);
            #endregion

            Lesson lesson = new Lesson();
            lesson.OnStart += OnStart_Program;
            lesson.OnLessonEvent += Method;
            lesson.OnLessonEvent += (sender, e) => Console.WriteLine("Anonym on event");
            OnLessonStart onLesson = () => Console.WriteLine("in object");
            lesson?.OnStart?.Invoke();

            MathOperation plus = (a, b) => a + b;
            MathOperation minus = (a, b) => a - b;
            MathOperation miltiply = (a, b) => a * b;
            MathOperation divide = (arg1, arg2) => arg1 / arg2;

            PersonUpdate update = (p) => p.Q++; // 1 in chain
            update += (p) => p.Age--; // 2 in chain 
            update += update; // 4 in chain
            update += update; // 8 in chain
            update += (p) => Console.WriteLine(p.ToString());

            Person person = new Person("QWERTY", 25, 0);
            update(person);


            
        }

        static void Method(object sender, EventArgs eventArgs) 
        {
            var lesson = (Lesson)sender;
            Console.WriteLine($"Method_Program {lesson.OnStart.Method}");
        }

        static void OnStart_Program()
        {
            Console.WriteLine("OnStart_Program method");
        }
    }
}
