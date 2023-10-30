using eSports.DAL.Interfaces;
using eSports.Domain.Teams.Entity;

namespace eSports.DAL.Repositories
{
    public class TeamRepository : IBaseRepository<TeamEntity>
    {
        private readonly AppDbContext _appDbContext;

        public TeamRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public async Task Create(TeamEntity entity)
        {
            await _appDbContext.Teams.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(TeamEntity entity)
        {
            _appDbContext.Teams.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<TeamEntity> GetAll()
        {
            return _appDbContext.Teams;
        }

        public async Task<TeamEntity> Update(TeamEntity entity)
        {
            _appDbContext.Teams.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Attach(TeamEntity entity)
        {
            _appDbContext.Teams.Attach(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
