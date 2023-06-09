using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using New_Zealand.webApi.Data;
using New_Zealand.webApi.Models.Domain;
using New_Zealand.webApi.Models.DTO;
using System;

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

        public async Task<List<Walk>> GetWalksAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walk= _dbContext.Walks.Include("Difficulty").Include("Regions").AsQueryable();

            //filtre(string? filterOn = null, string? filterQuery = null)
            if (string.IsNullOrWhiteSpace(filterOn)==false&&string.IsNullOrWhiteSpace(filterQuery)==false )
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = walk.Where(x => x.Name.Contains(filterQuery));
                }
            }


            //tri

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk= isAscending?walk.OrderBy(x=>x.LengthInKm):walk.OrderByDescending(x=>x.LengthInKm);
                }
            }


            // pagination(int pageNumber = 1, int pageSize = 1000)

            var SkipResult = (pageNumber - 1) * pageSize;
            return await walk.Skip(SkipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetOneWalksAsync(Guid id)
        {
            return await _dbContext.Walks
                .Include("Difficulty")
                .Include("Regions")
                .FirstOrDefaultAsync(x=>x.Id == id);
        }


        public async Task<Walk?> UpdateWalksAsync(Guid id, Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id==id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
