using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Split
{
    public class ExpenseRecord
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        // Expense ID linked to this record
        public int SplitExpenseId { get; set; }
        // ID for the person linked to this expense record
        public int SplitPersonId { get; set; }
        public double SplitAmount { get; set; }
    }
}
