using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Country: {Country}";
        }
    }
}
