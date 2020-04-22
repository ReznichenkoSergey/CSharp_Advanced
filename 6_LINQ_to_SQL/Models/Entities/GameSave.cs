using IteaLinqToSql.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace IteaLinqToSql.Models.Entities
{
    public class GameSave: IIteaModel
    {
        [Key] public int Id { get; set; }
        public DateTime Date { get; set; }
        public string GameBaseContent { get; set; }
        public string MapContent { get; set; }
    }
}
