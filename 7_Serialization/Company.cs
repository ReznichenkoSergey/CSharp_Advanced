using System;
using System.Collections.Generic;
using System.Linq;

namespace IteaSerialization
{
    public class Company : IModel, ILinkUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();

        protected Company() { }

        public Company(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        /// <summary>
        /// Обновление ссылок после десериализации
        /// </summary>
        public void UpdateLinks()
        {
            Departments.ForEach(x => { 
                x.Company = this;
                x.Persons
                    .ForEach(y => { y.Department = x; });
            });
        }

        public override bool Equals(object obj)
        {
            if (obj is Company)
            {
                Company temp = (Company)obj;
                bool depEquals = true;
                if (Departments.Count == temp.Departments.Count)
                {
                    Departments.ForEach(x =>
                    {
                        if (!temp.Departments.Any(y => y.Equals(x)))
                        {
                            depEquals = false;
                        }
                    }
                    );
                }
                else
                    depEquals = false;
                return
                    depEquals &&
                    Name.Equals(temp.Name) &&
                    Id.Equals(temp.Id);
            }
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Id.ToString().Substring(0, 5)}_{Name}";
        }

    }
}
