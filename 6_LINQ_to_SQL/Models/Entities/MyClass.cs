using IteaLinqToSql.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IteaLinqToSql.Models.Entities
{
    public class MyClass: IIteaModel
    {
        [Key] public int Id { get; set; }
        public string Cipher { get; set; }
        public int Age { get; set; }
        public string Comment { get; set; }
    }
}
