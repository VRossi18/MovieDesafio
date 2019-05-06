using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MovieDesafio
{
    [Table("Itens")]
    public class Itens
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataLancamento { get; set; }
    }
}
