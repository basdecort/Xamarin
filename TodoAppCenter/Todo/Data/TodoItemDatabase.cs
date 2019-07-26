using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Data;
using SQLite;

namespace Todo
{
	public class TodoItemDatabase
	{

        private TimeSpan _cachingTime = TimeSpan.FromDays(7);
		public TodoItemDatabase()
		{
		}

		public async Task<List<TodoItem>> GetItemsAsync()
		{
            var result = await Data.ListAsync<TodoItem>(DefaultPartitions.UserDocuments);

            return result.Select(r => r.DeserializedValue).ToList();
        }

		public async Task<List<TodoItem>> GetItemsNotDoneAsync()
		{
            var result = await Data.ListAsync<TodoItem>(DefaultPartitions.AppDocuments);

            return result.Select(r => r.DeserializedValue).Where(item => !item.Done).ToList();
        }

		public async Task<TodoItem> GetItemAsync(Guid id)
		{
            var item = await Data.ReadAsync<TodoItem>(id.ToString(), DefaultPartitions.AppDocuments);
            return item.DeserializedValue;
        }

		public async Task SaveItemAsync(TodoItem item)
		{
            if (item.ID == Guid.Empty)
            {
                item.ID = Guid.NewGuid();
                await Data.CreateAsync(item.ID.ToString(), item, DefaultPartitions.AppDocuments, new WriteOptions(_cachingTime));
            }
            else
            {
                await Data.ReplaceAsync(item.ID.ToString(), item, DefaultPartitions.AppDocuments);
            }
		}

		public Task DeleteItemAsync(TodoItem item)
		{
            return Data.DeleteAsync<TodoItem>(item.ID.ToString(), DefaultPartitions.AppDocuments);
        }
	}
}

