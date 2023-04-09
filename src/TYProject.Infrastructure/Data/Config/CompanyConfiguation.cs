using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TYProject.Core.CompanyAggregate;

namespace TYProject.Infrastructure.Data.Config;
internal class CompanyConfiguation : IEntityTypeConfiguration<Company>
{
  public void Configure(EntityTypeBuilder<Company> entity)
  {
    entity.ToTable("Company");

    entity.Property(e => e.CreatedOnDate).HasColumnType("datetime");
    entity.Property(e => e.Description).HasMaxLength(500);
    entity.Property(e => e.LastModifiedOnDate).HasColumnType("datetime");
    entity.Property(e => e.Name)
        .IsRequired()
        .HasMaxLength(100);
    entity.Property(e => e.RowVer)
        .IsRequired()
        .IsRowVersion()
    .IsConcurrencyToken();

    entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CompanyCreatedByNavigation)
        .HasForeignKey(d => d.CreatedBy)
        .OnDelete(DeleteBehavior.ClientSetNull);

    entity.HasOne(d => d.LastModifiedByNavigation).WithMany(p => p.CompanyLastModifiedByNavigation).HasForeignKey(d => d.LastModifiedBy);
  }
}
