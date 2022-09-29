using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

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
