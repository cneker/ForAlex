using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges) => await GetAll(trackChanges).ToListAsync();
        public IEnumerable<Employee> GetAllEmployees(bool trackChanges) => GetAll(trackChanges).Include(e => e.Position).ToList();
        public async Task<Employee> GetEmployeeAsync(Guid id, bool trackChanges) => await GetByCondition(e => e.Id == id, trackChanges).SingleOrDefaultAsync();
        public void CreateEmployee(Employee employee) => Create(employee);
        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
