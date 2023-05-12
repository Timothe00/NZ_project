using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using New_Zealand.webApi.Data;
using New_Zealand.webApi.Models.Domain;

namespace New_Zealand.webApi.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly New_ZealandDbContext dbContext;
        public SQLRegionRepository(New_ZealandDbContext dbContext)
        {
            this.dbContext= dbContext;
        }
        public async Task<List<Regions>> GetAllRegionsAsync()
        {
            return await dbContext.Regionss.ToListAsync();
        }

        public async Task<Regions?>GetOneByIdRegionsAsync(Guid id)
        {
            return await dbContext.Regionss.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Regions> CreateRegionAsync(Regions regions)
        {
            await dbContext.Regionss.AddAsync(regions);
            await dbContext.SaveChangesAsync();
            return regions;
        }

        public async Task<Regions?> UpdateRegionAsync(Guid id, Regions regions)
        {
            var existingRegion = await dbContext.Regionss.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = regions.Code;
            existingRegion.Name = regions.Name;
            existingRegion.RegionImageUrl= regions.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regions;

        }


        public async Task<Regions?> DeleteRegionAsync(Guid id)
        {
            var existingRegion = await dbContext.Regionss.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regionss.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
