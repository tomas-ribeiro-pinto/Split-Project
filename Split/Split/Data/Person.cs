using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace Split
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhotoID { get; set; }

        /// <summary>
        /// This method sums all of the split amounts for each
        /// expense record in a list
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        public static double SumOfExpenses(List<ExpenseRecord> records)
        {
            double sum = 0;

            foreach (ExpenseRecord record in records)
            {
                sum += record.SplitAmount;
            }

            return sum;
        }
    }
}
