using MarkingTestProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarkingTestProject.Infrastructure.Configurations
{
    public class BottleConfiguration : IEntityTypeConfiguration<Bottle>
    {
        public void Configure(EntityTypeBuilder<Bottle> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Box)
                .WithMany(x => x.Bottles)
                .HasForeignKey(x => x.BoxId);
        }
    }
}
