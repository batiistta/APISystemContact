using APISystemContact.Data.Map;
using APISystemContact.Models;
using Microsoft.EntityFrameworkCore;

namespace APISystemContact.Data
{
    public class APISystemContactDBContext : DbContext
    {
        public APISystemContactDBContext(DbContextOptions<APISystemContactDBContext> options) 
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ContactMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
