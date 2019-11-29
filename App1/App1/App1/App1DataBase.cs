using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class App1DataBase
    {
        readonly SQLiteAsyncConnection database;

        public App1DataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<App1Item>().Wait();
        }

        public Task<List<App1Item>> GetItemsAsync()
        {
            return database.Table<App1Item>().ToListAsync();
        }

        public async Task<List<App1Item>> GetUnPostedAppItems()
        {
            var unPosted = await database.Table<App1Item>().Where(x => x.Posted == false).ToListAsync();
            return unPosted;
        }

        public Task<List<App1Item>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<App1Item>("SELECT * FROM [App1Item] ");
        }

        public Task<App1Item> GetItemAsync(int id)
        {
            return database.Table<App1Item>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(App1Item item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

    }
}

