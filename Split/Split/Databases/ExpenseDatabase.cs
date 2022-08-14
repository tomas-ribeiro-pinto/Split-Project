using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Split
{
    public class ExpenseDatabase
    {
        readonly SQLiteAsyncConnection _database;

        Expense expense1 = new Expense
        {
            Title = "Coffee Shop",
            Amount = 2.35
        };

        Expense expense2 = new Expense
        {
            Title = "Dinner with Mary",
            Amount = 56.00
        };

        public ExpenseDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();

            if (_database.Table<Expense>().CountAsync().Result == 0)
            {
                _database.InsertAsync(expense1).Wait();
                _database.InsertAsync(expense2).Wait();
            }
        }

        public Task<List<Expense>> GetExpenseAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task<int> SaveExpenseAsync(Expense expense)
        {
            return _database.InsertAsync(expense);
        }

        public Task<int> DeleteExpenseEntry(Expense expense)
        {
            return _database.DeleteAsync(expense);
        }

        public Task<int> DeleteAllExpense()
        {
            return _database.DeleteAllAsync<Expense>();
        }
    }

}