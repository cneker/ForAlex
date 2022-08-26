using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PosititonRepository : RepositoryBase<Position>
    {
        public PosititonRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Position>> GetAllPositionsAsync(bool trackChanges) => await GetAll(trackChanges).ToListAsync();
        public IEnumerable<Position> GetAllPositions(bool trackChanges) => GetAll(trackChanges).ToList();
        public void CreatePosition(Position position) => Create(position);
        public void DeletePosition(Position position) => Delete(position);
    }
}
