using Microsoft.EntityFrameworkCore;
using New_Zealand.webApi.Models.Domain;

namespace New_Zealand.webApi.Data
{
    public class New_ZealandDbContext:DbContext
    {

        public virtual DbSet<Difficulty> Difficulties { get; set; }
        public virtual DbSet<Regions> Regionss { get; set; }
        public virtual DbSet<Walk> Walks { get; set; }

        public New_ZealandDbContext(DbContextOptions DbContextOptions):base(DbContextOptions)
        {

        }

    }
}
