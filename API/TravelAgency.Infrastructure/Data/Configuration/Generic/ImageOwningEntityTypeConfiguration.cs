using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Infrastructure.Data.Configuration.Generic
{
    public class ImageOwningEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IImageOwningEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.OwnsMany(c => c.Images, b =>
            {
                b.HasOne<Image>()
                    .WithMany()
                    .IsRequired(true)
                    .HasForeignKey(c => c.ImageId);

                b.WithOwner();
            });
        }
    }
}
