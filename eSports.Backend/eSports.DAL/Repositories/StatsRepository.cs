using eSports.DAL.Interfaces;
using eSports.Domain.Stats.Entity;

namespace eSports.DAL.Repositories
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

        public async Task Attach(StatsEntity entity)
        {
            _appDbContext.Stats.Attach(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
