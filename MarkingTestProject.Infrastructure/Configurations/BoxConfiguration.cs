using MarkingTestProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarkingTestProject.Infrastructure.Configurations
{
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        public void Configure(EntityTypeBuilder<Box> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasMany(x => x.Bottles)
                .WithOne(x => x.Box)
                .HasForeignKey(x => x.BoxId);

            builder
                .HasOne(x => x.Pallet)
                .WithMany(x => x.Boxes)
                .HasForeignKey(x => x.PalletId);
        }
    }
}
