using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Split
{
    public class PeopleDatabase
    {
        readonly SQLiteAsyncConnection _database;

        People people1 = new People
        {
            Name = "Ana",
        };

        People people2 = new People
        {
            Name = "Roberto",
        };

        public PeopleDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<People>().Wait();

            if (_database.Table<People>().CountAsync().Result == 0)
            {
                _database.InsertAsync(people1).Wait();
                _database.InsertAsync(people2).Wait();
            }
        }

        public Task<List<People>> GetPeopleAsync()
        {
            return _database.Table<People>().ToListAsync();
        }

        public Task<int> SavePeopleAsync(People people)
        {
            return _database.InsertAsync(people);
        }

        public Task<People> GetItemAsync(int id)
        {
            return _database.Table<People>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        /**
        public Task<People> SavePeopleAsync(Object people)
        {
            return _database.Fin(people);
        }
        
        /**
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

        public Task<int> DeleteExpenseEntry(Expense expense)
        {
            return _database.DeleteAsync(expense);
        }

        public Task<int> DeleteAllExpense()
        {
            return _database.DeleteAllAsync<Expense>();
        }
        **/
    }

}