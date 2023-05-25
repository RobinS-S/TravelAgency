using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Infrastructure.Data.Configuration
{
    public class ResidenceEntityTypeConfiguration : IEntityTypeConfiguration<Residence>
    {
        public void Configure(EntityTypeBuilder<Residence> builder)
        {
            builder.OwnsMany(r => r.Names, b =>
            {
                b.Property(n => n.IsoLanguageName).HasMaxLength(3);
                b.Property(n => n.Text).HasMaxLength(128);
            });

            builder.OwnsMany(r => r.Descriptions, b =>
            {
                b.Property(n => n.IsoLanguageName).HasMaxLength(3);
                b.Property(n => n.Text).HasMaxLength(256);
            });
        }
    }
}
