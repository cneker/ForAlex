using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ItemRepository : RepositoryBase<Item>
    {
        public ItemRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync() => await GetAll().ToListAsync();
        public async Task<IEnumerable<Item>> GetAllItemsOrderedByPriceAscAsync() => await GetAll().OrderBy(i => i.Price).ToListAsync();
        public void CreateItem(Item item) => Create(item);
        public void DeleteItem(Item item) => Delete(item);
    }
}
