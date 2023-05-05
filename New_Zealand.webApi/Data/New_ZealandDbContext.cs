using Microsoft.EntityFrameworkCore;
using New_Zealand.webApi.Models.Domain;

namespace New_Zealand.webApi.Data
{
    public class New_ZealandDbContext:DbContext
    {



        public New_ZealandDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Regions> Regionss { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
