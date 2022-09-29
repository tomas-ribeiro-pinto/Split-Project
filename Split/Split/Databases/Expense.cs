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
        public int ExpensePersonId { get; set; }
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

        public string GetPerson()
        {
            return App.PeopleDatabase.GetItemAsync(ExpensePersonId).Result.Name;
        }
    }
}
