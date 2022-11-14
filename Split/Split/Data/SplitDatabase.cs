using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Split
{
    public class SplitDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public SplitDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();
            _database.CreateTableAsync<Trip>().Wait();
            _database.CreateTableAsync<ExpenseRecord>().Wait();
            _database.CreateTableAsync<Person>().Wait();
        }

        #region Expense CRUD operations

        public Task<List<Expense>> GetExpenseListAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task<Expense> GetExpenseAsync(int id)
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

        public Task<int> DeleteExpenseAsync(Expense expense)
        {
            foreach (ExpenseRecord record in GetRecordList_byExpense(expense.ID).Result)
            {
                DeleteRecordAsync(record.ID);
            }
            return _database.DeleteAsync(expense);
        }

        public Task<int> DeleteAllExpenseAsync()
        {
            DeleteAllRecordAsync();
            return _database.DeleteAllAsync<Expense>();
        }

        #endregion

        #region ExpenseRecord CRUD operations

        public Task<List<ExpenseRecord>> GetRecordListAsync()
        {
            return _database.Table<ExpenseRecord>().ToListAsync();
        }

        /// <summary>
        ///     Returns all the records from a specific person ID
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>List with records of expenses made by 'person'</returns>
        public Task<List<ExpenseRecord>> GetRecordList_byPerson(int personId)
        {
            return _database.Table<ExpenseRecord>().
                Where(i => i.PersonID == personId).ToListAsync();
        }

        /// <summary>
        ///     Returns all the records in an expense
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns>List with records from an 'expense'</returns>
        public Task<List<ExpenseRecord>> GetRecordList_byExpense(int expenseId)
        {
            return _database.Table<ExpenseRecord>().
                Where(i => i.ExpenseID == expenseId).ToListAsync();
        }

        public Task<ExpenseRecord> GetRecordAsync(int id)
        {
            return _database.Table<ExpenseRecord>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateRecordAsync(ExpenseRecord record)
        {
            return _database.UpdateAsync(record);
        }

        public Task<int> SaveRecordAsync(ExpenseRecord record)
        {
            return _database.InsertAsync(record);
        }

        public Task<int> DeleteRecordAsync(ExpenseRecord record)
        {
            return _database.DeleteAsync(record);
        }

        public Task<int> DeleteRecordAsync(int recordId)
        {
            ExpenseRecord record = GetRecordAsync(recordId).Result;
            return _database.DeleteAsync(record);
        }

        public Task<int> DeleteAllRecordAsync()
        {
            return _database.DeleteAllAsync<ExpenseRecord>();
        }

        #endregion

        #region Trip CRUD operations

        public Task<List<Trip>> GetTripListAsync()
        {
            return _database.Table<Trip>().ToListAsync();
        }

        public Task<Trip> GetTripAsync(int id)
        {
            return _database.Table<Trip>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTripAsync(Trip trip)
        {
            return _database.InsertAsync(trip);
        }

        public Task<int> UpdatePersonAsync(Trip trip)
        {
            return _database.UpdateAsync(trip);
        }

        public Task<int> DeletePersonAsync(Trip trip)
        {
            return _database.DeleteAsync(trip);
        }

        #endregion

        #region People CRUD operations

        public Task<List<Person>> GetPersonListAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        public Task<Person> GetPersonAsync(int id)
        {
            return _database.Table<Person>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }

        public Task<int> UpdatePersonAsync(Person person)
        {
            return _database.UpdateAsync(person);
        }

        public Task<int> DeletePersonAsync(Person person)
        {
            return _database.DeleteAsync(person);
        }

        #endregion

    }

}