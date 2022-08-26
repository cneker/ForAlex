using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Guid PositionId { get; set; }
        public Position? Position { get; set; }

        public ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {FirstName + LastName}, Age: {Age}, Position id: {PositionId}";
        }
    }
}
