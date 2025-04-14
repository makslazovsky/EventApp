using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.PasswordHash).IsRequired();

            builder.Property(u => u.RefreshToken).HasMaxLength(200);

            builder.Property(u => u.RefreshTokenExpiry);

            builder.HasOne(u => u.Participant)
                   .WithOne(p => p.User)
                   .HasForeignKey<Participant>(p => p.UserId);
        }
    }
}
