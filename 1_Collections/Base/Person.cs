using ITEA_Collections.Base;

namespace ITEA_Collections
{
    public class Person : IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int Q { get; set; }

        public Person()
        {
        }

        public Person(string name, int age, int q)
        {
            Name = name;
            Age = age;
            Q = q;
        }



        public override string ToString()
        {
            return $"{Name}: {Age}, Q: {Q}";
        }
    }
}
