using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IDisposable
    {
        private RepositoryContext _context;
        private EmployeeRepository _employeeRepository;
        private ItemRepository _itemRepository;
        private OrderRepository _orderRepository;
        private OrganizationRepository _organizationRepository;
        private PosititonRepository _positionRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
        }

        public EmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_context);
                return _employeeRepository;
            }
        }
        public ItemRepository ItemRepository
        {
            get
            {
                if (_itemRepository == null)
                    _itemRepository = new ItemRepository(_context);
                return _itemRepository;
            }
        }
        public OrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }
        public OrganizationRepository OrganizationRepository
        {
            get
            {
                if (_organizationRepository == null)
                    _organizationRepository = new OrganizationRepository(_context);
                return _organizationRepository;
            }
        }
        public PosititonRepository PosititonRepository
        {
            get
            {
                if (_positionRepository == null)
                    _positionRepository = new PosititonRepository(_context);
                return _positionRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();


    }
}
