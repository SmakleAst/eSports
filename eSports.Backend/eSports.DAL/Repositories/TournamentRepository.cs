﻿using eSports.Domain.Tournament.Entity;
using eSports.DAL.Interfaces;

namespace eSports.DAL.Repositories
{
    public class TournamentRepository : IBaseRepository<TournamentEntity>
    {
        private readonly AppDbContext _appDbContext;

        public TournamentRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public async Task Create(TournamentEntity entity)
        {
            await _appDbContext.Tournaments.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(TournamentEntity entity)
        {
            _appDbContext.Tournaments.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<TournamentEntity> GetAll()
        {
            return _appDbContext.Tournaments;
        }

        public async Task<TournamentEntity> Update(TournamentEntity entity)
        {
            _appDbContext.Tournaments.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Attach(TournamentEntity entity)
        {
            _appDbContext.Tournaments.Attach(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
