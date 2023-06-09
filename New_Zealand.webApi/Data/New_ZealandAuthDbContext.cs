using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace New_Zealand.webApi.Data
{
    public class New_ZealandAuthDbContext:IdentityDbContext
    {
        public New_ZealandAuthDbContext(DbContextOptions<New_ZealandAuthDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "7c33e119-b58c-4c3a-96c1-a5aa0d5b546d";
            var writerRoleId = "38297c20-8975-4b45-b903-49907d7aafac";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name= "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id= writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
