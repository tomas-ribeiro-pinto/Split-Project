using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Split
{
    public class Trip
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        // Added code to serialize list of person taking part in this trip
        [TextBlob("PeopleBlobbed")]
        public List<int> People { get; set; }
        public string PeopleBlobbed { get; set; } // serialized People

        public void AddPerson(Person person)
        {
            People.Add(person.ID);
        }
    }
}
