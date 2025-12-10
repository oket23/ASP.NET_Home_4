using Home_4.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Home_4.DAL.Configurations;

public class MembershipsConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MemberId)
            .IsRequired();

        builder.Property(x => x.StatusEnum)
            .IsRequired();

        builder.Property(x => x.StartsOn)
            .IsRequired();

        builder.Property(x => x.EndsOn)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.FrozenAtUtc)
            .IsRequired(false);

        builder.HasIndex(x => x.MemberId);

        // Якщо хочеш check constraint (працює в SQL Server / PostgreSQL, залежить від провайдера)
        builder.ToTable(t => t.HasCheckConstraint("CK_Memberships_EndsOn_GTE_StartsOn", "\"EndsOn\" >= \"StartsOn\""));

        builder.HasQueryFilter(x => x.DeletedAtUtc == null);
    }
}