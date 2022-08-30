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
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasOne(p => p.Author)
                .WithOne(a => a.Photo)
                .HasForeignKey<Photo>(p => p.AuthorId);

            builder.Property(p => p.OriginalSize)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.DateOfCreation)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(p => p.Cost)
                .HasColumnType("decimal(5,2)");
        }
    }
}
