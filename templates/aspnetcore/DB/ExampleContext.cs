using Business;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions options)
            : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExampleBusinessModel>()
                .HasData(
                new ExampleBusinessModel
                {
                    Age = 20,
                    Email = "ooof",
                    Id = 1,
                    Name = "kek"
                });
        }
    }
}
