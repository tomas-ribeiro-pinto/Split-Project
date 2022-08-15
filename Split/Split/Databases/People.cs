using System.Collections.Generic;
using SQLite;

namespace Split
{
    public class People
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }

        //public Dictionary<Expense, double> ExpenseRecord { get; set; }

        public People()
        {
            //ExpenseRecord = new Dictionary<Expense, double>();
        }
    }
}
