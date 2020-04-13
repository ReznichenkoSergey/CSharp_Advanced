using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace IteaSerialization
{
    [Serializable]
    public class Department : IModel, ILinkUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Person> Persons { get; set; } = new List<Person>();

        [JsonIgnore]
        [XmlIgnore]
        public Company Company;
        protected Department() { }

        public Department(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        ///
        ///Set company for department
        ///
        public Department(string name, Company company) : this(name)
        {
            SetCompany(company);
        }

        public void SetCompany(Company company)
        {
            Company = company;
            Company.Departments.Add(this);
        }

        public override bool Equals(object obj)
        {
            if (obj is Department)
            {
                Department temp = (Department)obj;
                bool personEquals = true;
                if (Persons.Count == temp.Persons.Count)
                {
                    Persons.ForEach(x =>
                    {
                        if (!temp.Persons.Any(y => y.Equals(x)))
                        {
                            personEquals = false;
                        }
                    }
                    );
                }
                else
                    personEquals = false;
                return
                    personEquals &&
                    Id.Equals(temp.Id)&&
                    Name.Equals(temp.Name, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return base.Equals(obj);
        }

        /// <summary>
        /// Обновление ссылок после десериализации
        /// </summary>
        public void UpdateLinks()
        {
            Persons.ForEach(x => {x.Department = this; });
        }

        public override string ToString()
        {
            return $"{Id.ToString().Substring(0, 5)}_{Name}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
