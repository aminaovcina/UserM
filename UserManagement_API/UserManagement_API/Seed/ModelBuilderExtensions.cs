using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Entities.User;

namespace UserManagement_API.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
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
                    Id =3,
                    Code = "PUB",
                    Description = "Read"
                },
                new Permission
                {
                    Id=4,
                    Code = "DEL",
                    Description = "Delete"
                }
            );
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Active" },
                new Status { Id = 2, Name = "Inactive"}       
            );
        }
    }
}
