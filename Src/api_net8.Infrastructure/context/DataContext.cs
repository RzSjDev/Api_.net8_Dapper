using api.net.Models;
using api_net8.Infrastructure.context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.net.Data
{
    public class DataContext : DbContext
    {
 
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersContact>().HasOne(c => c.userAuth).WithMany(c => c.usersContacts).HasForeignKey(c=>c.UserAuthId);
        }

      

        public DbSet<UsersContact> usersContacts { get; set; }
        public DbSet<UserAuth> userAuths { get; set; }
    }
}

