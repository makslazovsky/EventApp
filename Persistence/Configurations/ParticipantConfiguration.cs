﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(100);

            builder.HasOne(p => p.Event)
                   .WithMany(e => e.Participants)
                   .HasForeignKey(p => p.EventId);

            builder.HasOne(p => p.User)
                  .WithMany(u => u.Participants)
                  .HasForeignKey(p => p.UserId);

            // ✅ Уникальный индекс (UserId + EventId)
            builder.HasIndex(p => new { p.UserId, p.EventId }).IsUnique();

        }
    }
}
