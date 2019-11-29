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
            database.CreateTableAsync<OrderItem>().Wait();
        }

        public Task<List<OrderItem>> GetItemsAsync()
        {
            return database.Table<OrderItem>().ToListAsync();
        }

        public async Task<List<OrderItem>> GetUnPostedAppItems()
        {
            var unPosted = await database.Table<OrderItem>().Where(x => x.Posted == false).ToListAsync();
            return unPosted;
        }

        public Task<OrderItem> GetItemAsync(int id)
        {
            return database.Table<OrderItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(OrderItem item)
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

