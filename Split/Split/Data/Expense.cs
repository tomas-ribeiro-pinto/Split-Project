using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Split
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        // ID of the person who made the expense
        public int PersonID { get; set; }
        public int TripID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpense { get; set; }

        public Expense()
        {
            DateCreated = new DateTime();
            DateExpense = new DateTime();
        }

        public Expense GetExpense()
        {
            return this;
        }

        public string GetPersonName()
        {
            return App.SplitDatabase.GetPersonAsync(PersonID).Result.Name;
        }

        public static double SumOfExpenses(List<Expense> expenses)
        {
            double total = 0;

            foreach (Expense expense in expenses)
            {
                total += expense.Amount;
            }

            return total;
        }
    }
}
