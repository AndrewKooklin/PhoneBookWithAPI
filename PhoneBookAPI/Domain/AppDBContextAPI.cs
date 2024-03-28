﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBookAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain
{
    public class AppDBContextAPI : IdentityDbContext
    {
        public AppDBContextAPI(DbContextOptions<AppDBContextAPI> options) : base(options) { }

        public DbSet<PhoneBookRecord> PhoneBookRecords { get; set; }

        //public DbSet<AspNetRoles> PhoneBookRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PhoneBookRecord>().HasData(new PhoneBookRecord
            {
                Id = 1,
                LastName = "Ivanov",
                FirstName = "Ivan",
                FathersName = "Ivanovich",
                PhoneNumber = "+79154587481",
                Address = "Mocsow, st.Lenina 10 f.11",
                Description = "1 Ivanov Ivan Ivanovich"
            });

            builder.Entity<PhoneBookRecord>().HasData(new PhoneBookRecord
            {
                Id = 2,
                LastName = "Petrov",
                FirstName = "Petr",
                FathersName = "Petrovich",
                PhoneNumber = "+79154587482",
                Address = "Saint-Petersburg, st.Lenina 11 f.12",
                Description = "2 Petrov Petr Petrovich"
            });

            builder.Entity<PhoneBookRecord>().HasData(new PhoneBookRecord
            {
                Id = 3,
                LastName = "Sidorov",
                FirstName = "Sidor",
                FathersName = "Sidorovich",
                PhoneNumber = "+79154587483",
                Address = "Rostov, st.Lenina 12 f.13",
                Description = "3 Sidorov Sidor Sidorovich"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "6d5c54e4-667f-4b61-9ac9-d61039cdf950"
                //Description = "This role can perform all operations"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "302dc497-bf7b-49fa-af47-cc1123b3fe8e"
                //Description = "This role can add PhoneBookRecord"
            });
        }
    }
}
