using Crm.Maui.Models;
using Microsoft.EntityFrameworkCore;

/*cuando se necesite acceder a la bd se hara a través del dbContext accediendo a su propiedad en este caso
customers*/
namespace Crm.Maui.Persistence
{
    public class CrmDbContext :DbContext
    {
        //Entidades
        public DbSet<Customer> Customers { get; set;  }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "crm.db");
            optionsBuilder.UseSqlite($"FileName={dbPath}");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .HasData(
                new Customer
                {
                    Id = 1,
                    FirstName="Bruce",
                    LastName = "Wayne",
                    Email ="bruce.wayne@gmail.com"
                },
                new Customer
                 {
                   Id = 2,
                   FirstName = "Peter",
                   LastName = "Parker",
                   Email = "peter.araña@gmail.com"
                 },
                new Customer
                {
                    Id = 3,
                    FirstName = "Tony",
                    LastName = "Stark",
                    Email = "tony.sk@gmail.com"
                });
        }
    }
}
