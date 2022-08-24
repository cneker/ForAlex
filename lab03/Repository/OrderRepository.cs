using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() => await GetAll().ToListAsync();
        public void CreateOrder(Order order) => Create(order);
        public void DeleteOrder(Order order) => Delete(order);
    }
}
