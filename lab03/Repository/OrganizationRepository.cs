using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrganizationRepository : RepositoryBase<Organization>
    {
        public OrganizationRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync() => await GetAll().ToListAsync();
        public void CreateOrganization(Organization organization) => Create(organization);
        public void DeleteOrganization(Organization organization) => Delete(organization);
    }
}
