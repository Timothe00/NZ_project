using Microsoft.EntityFrameworkCore;
using New_Zealand.webApi.Data;
using New_Zealand.webApi.Models.Domain;
using New_Zealand.webApi.Models.DTO;

namespace New_Zealand.webApi.Repositories
{
    public class SQLWalksRepository : IWalksRepository
    {

        private readonly New_ZealandDbContext _dbContext;
        //on va injecter le DBCONTEXT dans le constructeur
        public SQLWalksRepository(New_ZealandDbContext new_ZealandDbContext)
        {
            this._dbContext = new_ZealandDbContext;
        }
        public async Task<Walk> CreateWalksAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetWalksAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include(x=>x.Regions).ToListAsync();
        }

        public async Task<Walk?> GetOneWalksAsync(Guid id)
        {
            return await _dbContext.Walks
                .Include("Difficulty")
                .Include(x => x.Regions)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
