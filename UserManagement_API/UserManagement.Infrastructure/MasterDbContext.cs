using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities.User;

namespace UserManagement.Infrastructure
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Permission> Permission { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Permission>().HasData(
               new Permission
               {
                   Id = 1,
                   Code = "ADM",
                   Description = "Admin"
               },
               new Permission
               {
                   Id = 2,
                   Code = "USR",
                   Description = "Write"
               },
               new Permission
               {
                   Id = 3,
                   Code = "PUB",
                   Description = "Read"
               },
               new Permission
               {
                   Id = 4,
                   Code = "DEL",
                   Description = "Delete"
               }
           );
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Active" },
                new Status { Id = 2, Name = "Inactive" }
            );
        }
    }
}
