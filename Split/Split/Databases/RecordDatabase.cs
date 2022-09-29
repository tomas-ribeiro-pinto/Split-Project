using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Split
{
    public class RecordDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public RecordDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ExpenseRecord>().Wait();
        }

        public Task<List<ExpenseRecord>> GetExpenseRecordAsync()
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
                Where(i => i.SplitPersonId == personId).ToListAsync();
        }

        /// <summary>
        ///     Returns all the records in an expense
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns>List with records from an 'expense'</returns>
        public Task<List<ExpenseRecord>> GetRecordList_byExpense(int expenseId)
        {
            return _database.Table<ExpenseRecord>().
                Where(i => i.SplitExpenseId == expenseId).ToListAsync();
        }

        public Task<ExpenseRecord> GetItemAsync(int id)
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

        public Task<int> DeleteRecord(ExpenseRecord record)
        {
            return _database.DeleteAsync(record);
        }

        public Task<int> DeleteRecord(int recordId)
        {
            ExpenseRecord record = GetItemAsync(recordId).Result;
            return _database.DeleteAsync(record);
        }

        public Task<int> DeleteAllRecord()
        {
            return _database.DeleteAllAsync<ExpenseRecord>();
        }
    }

}