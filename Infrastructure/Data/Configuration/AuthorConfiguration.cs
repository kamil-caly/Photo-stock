using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.DateOfRegistration)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(a => a.FirstName)
                .IsRequired();

            builder.Property(a => a.NickName)
                .IsRequired();
        }
    }
}
