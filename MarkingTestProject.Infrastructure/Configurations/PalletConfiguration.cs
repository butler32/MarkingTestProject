using MarkingTestProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarkingTestProject.Infrastructure.Configurations
{
    public class PalletConfiguration : IEntityTypeConfiguration<Pallet>
    {
        public void Configure(EntityTypeBuilder<Pallet> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasMany(x => x.Boxes)
                .WithOne(x => x.Pallet)
                .HasForeignKey(x => x.PalletId);
        }
    }
}
