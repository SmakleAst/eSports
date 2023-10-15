using eSports.Backend.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSports.Backend.DAL.Repositories
{
    public class StatsRepository : IBaseRepository<StatsEntity>
    {
        private readonly AppDbContext _appDbContext;

        public StatsRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public async Task Create(StatsEntity entity)
        {
            await _appDbContext.Stats.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(StatsEntity entity)
        {
            _appDbContext.Stats.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<StatsEntity> GetAll()
        {
            return _appDbContext.Stats;
        }

        public async Task<StatsEntity> Update(StatsEntity entity)
        {
            _appDbContext.Stats.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
