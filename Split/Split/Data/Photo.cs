using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Split
{
    public class Photo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string URL { get; set; }
    }
}
