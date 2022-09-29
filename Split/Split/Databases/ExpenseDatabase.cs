using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Split
{
    public class ExpenseDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ExpenseDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();

        }

        public Task<List<Expense>> GetExpenseAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task<Expense> GetItemAsync(int id)
        {
            return _database.Table<Expense>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateExpenseAsync(Expense expense)
        {
            return _database.UpdateAsync(expense);
        }

        public Task<int> SaveExpenseAsync(Expense expense)
        {
            return _database.InsertAsync(expense);
        }

        public Task<int> DeleteExpense(Expense expense)
        {
            foreach (ExpenseRecord record in App.RecordDatabase.GetRecordList_byExpense(expense.ID).Result)
            {
                App.RecordDatabase.DeleteRecord(record.ID);
            }
            return _database.DeleteAsync(expense);
        }

        public Task<int> DeleteAllExpense()
        {
            App.RecordDatabase.DeleteAllRecord();
            return _database.DeleteAllAsync<Expense>();
        }
    }

}