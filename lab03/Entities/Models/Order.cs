using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Item name: {Item.Name}, Item price: {Item.Price}, Amount: {Amount}, Organization: {Organization.Name}, Employee: {Employee.LastName}";
        }
    }
}
