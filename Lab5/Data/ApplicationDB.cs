using Lab5.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Data
{
    public class ApplicationDB:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UsersInfo { get; set; }
        public ApplicationDB(DbContextOptions<ApplicationDB> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<UserInfo>().HasOne(p => p.User).WithOne(t=>t.UserInfo);
        }
    }
}
