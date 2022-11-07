using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace bART_TestProject.Models
{
    public class IncidentsContext : DbContext
    {
        public DbSet<Incident>? Incidents { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Contact>? Contacts { get; set; }

        public IncidentsContext(DbContextOptions<IncidentsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>()
                .HasIndex(acc => acc.Name)
                .IsUnique();

            Account colleagues = new Account { Id = 1, Name = "colleagues" };
            Account friends = new Account { Id = 2, Name = "friends" };

            Contact yurii = new Contact { Id = 1, FirstName = "Yurii", LastName = "Komenda", Email = "komenday@gmail.com", AccountId = 1 };
            Contact john = new Contact { Id = 2, FirstName = "John", LastName = "Doe", Email = "john@gmail.com", AccountId = 2 };
            Contact richard = new Contact { Id = 3, FirstName = "Richard", LastName = "Roe", Email = "rich@gmail.com", AccountId = 2 };

            builder.Entity<Contact>().HasData(yurii, john, richard);
            builder.Entity<Account>().HasData(colleagues, friends);

            base.OnModelCreating(builder);
        }
    }
}
