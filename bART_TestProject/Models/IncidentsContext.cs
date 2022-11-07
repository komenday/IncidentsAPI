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

            Account me = new Account { Id = 1, Name = "me" };
            Account friends = new Account { Id = 2, Name = "friends" };

            Contact yurii = new Contact { Id = 1, FirstName = "Yurii", LastName = "Komenda", Email = "komenday@gmail.com", AccountId = 1 };
            Contact nazar = new Contact { Id = 2, FirstName = "Nazar", LastName = "Datskiv", Email = "datckivn@gmail.com", AccountId = 2 };
            Contact max = new Contact { Id = 3, FirstName = "Max", LastName = "Slivinskiy", Email = "slvnskmx@gmail.com", AccountId = 2 };

            builder.Entity<Contact>().HasData(yurii, nazar, max);
            builder.Entity<Account>().HasData(me, friends);

            base.OnModelCreating(builder);
        }
    }
}
