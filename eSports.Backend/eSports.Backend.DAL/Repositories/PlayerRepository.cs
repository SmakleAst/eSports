using eSports.Backend.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSports.Backend.DAL.Repositories
{
    public class PlayerRepository : IBaseRepository<PlayerEntity>
    {
        private readonly AppDbContext _appDbContext;

        public PlayerRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public async Task Create(PlayerEntity entity)
        {
            await _appDbContext.Players.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(PlayerEntity entity)
        {
            _appDbContext.Players.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<PlayerEntity> GetAll()
        {
            return _appDbContext.Players;
        }

        public async Task<PlayerEntity> Update(PlayerEntity entity)
        {
            _appDbContext.Players.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
