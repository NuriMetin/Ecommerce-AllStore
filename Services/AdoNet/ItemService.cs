using DataAccess.Models;
using Services.Abstract;
using Services.AdoNet.Helpers.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.AdoNet
{
    public class ItemService : ItemQueries, IItemService
    {
        public Task<bool> CreateAsync(Item data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Item data)
        {
            throw new NotImplementedException();
        }
    }
}
