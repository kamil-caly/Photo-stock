﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class TextConfiguration : IEntityTypeConfiguration<Text>
    {
        public void Configure(EntityTypeBuilder<Text> builder)
        {
            builder.HasOne(t => t.Author)
                .WithOne(a => a.Text)
                .HasForeignKey<Text>(t => t.AuthorId);

            builder.Property(t => t.DateOfCreation)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(t => t.Cost)
                .HasColumnType("decimal(5,2)");
        }
    }
}
