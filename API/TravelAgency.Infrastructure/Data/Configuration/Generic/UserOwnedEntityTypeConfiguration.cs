using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Infrastructure.Data.Configuration.Generic
{
    internal class UserOwnedEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IUserOwnedEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasOne(i => i.Owner)
                .WithMany();
        }
    }
}
