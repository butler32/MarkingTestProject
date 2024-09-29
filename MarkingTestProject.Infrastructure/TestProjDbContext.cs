using MarkingTestProject.Domain.Entities;
using MarkingTestProject.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MarkingTestProject.Infrastructure
{
    public class TestProjDbContext : DbContext
    {
        public TestProjDbContext(DbContextOptions<TestProjDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Bottle> Bottles { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<Pallet> Pallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BottleConfiguration());
            modelBuilder.ApplyConfiguration(new BoxConfiguration());
            modelBuilder.ApplyConfiguration(new PalletConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
