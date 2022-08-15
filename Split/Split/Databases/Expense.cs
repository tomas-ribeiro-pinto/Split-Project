using System;
using System.Collections.Generic;
using SQLite;

namespace Split
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public int ExpensePersonID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpense { get; set; }

        //Record of People and split amount
        //public Dictionary<People,double> SplitRecord { get; set; }

        public Expense()
        {
            DateCreated = new DateTime();
            DateExpense = new DateTime();
            //SplitRecord = new Dictionary<People, double>();
        }
    }
}
