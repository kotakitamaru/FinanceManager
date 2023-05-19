using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager.Data
{
    public class FinancialOperationsDatabase
    {
        SQLiteAsyncConnection Database;

        public FinancialOperationsDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<FinancialOperationItem>();
        }
        public async Task<FinancialOperationItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<FinancialOperationItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(FinancialOperationItem item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(FinancialOperationItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
        public async Task<int> DeleteItemByIDAsync(int id)
        {
            await Init();
            var item = await Database.GetAsync<FinancialOperationItem>(id);
            if (item != null)
            {
                return await Database.DeleteAsync(item);
            }
            return 0;
        }

        public async Task<List<FinancialOperationItem>> GetTable()
        {
            await Init();
            return await Database.Table<FinancialOperationItem>().ToListAsync();
        }
        public async Task<List<FinancialOperationItem>> GetIncome()
        {
            await Init();
            return await Database.Table<FinancialOperationItem>().Where(x=>x.OperationType == true).ToListAsync();
        }
        public async Task<List<FinancialOperationItem>> GetSpends()
        {
            await Init();
            return await Database.Table<FinancialOperationItem>().Where(x=>x.OperationType == false).ToListAsync();
        }
        public async Task<bool> UpdateItemAsync(FinancialOperationItem item)
        {
            await Init();
            int rowsAffected = await Database.UpdateAsync(item);
            return rowsAffected > 0;
        }
    }


    public class FinancialOperationItem
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public bool OperationType { get; set;}
        public string amount { get; set; }
        [Ignore]
        public decimal Amount
        {
            get => decimal.Parse(amount);
            set => amount = value.ToString();
        }
        public string Type { get; set; }
        public string Description { get; set; }
        public string DateString { get; set; }
        

        [Ignore]
        public DateTime Date
        {
            get => DateTime.Parse(DateString);
            set => DateString = value.ToString();
        }
    }
}
