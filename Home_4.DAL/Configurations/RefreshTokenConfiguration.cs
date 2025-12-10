using Home_4.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Home_4.DAL.Configurations;

public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MemberId)
            .IsRequired();

        builder.Property(x => x.TokenHash)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc)
            .IsRequired();

        builder.Property(x => x.RevokedAtUtc)
            .IsRequired(false);

        builder.Property(x => x.RevokeReason)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.HasIndex(x => x.MemberId);
        builder.HasIndex(x => x.TokenHash);
        
        builder.HasOne(x => x.ReplacedByToken)
            .WithMany()
            .HasForeignKey(x => x.ReplacedByTokenId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => x.DeletedAtUtc == null);
    }
}