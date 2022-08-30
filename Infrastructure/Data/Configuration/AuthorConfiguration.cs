using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
