using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TYProject.Core.Company;

namespace TYProject.Infrastructure.Data.Config;
internal class CompanyConfiguation : IEntityTypeConfiguration<Company>
{
  public void Configure(EntityTypeBuilder<Company> builder)
  {
    builder.ToTable("Company");

    builder.Property(e => e.Name)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(e => e.RowVer)
      .IsRowVersion()
      .IsConcurrencyToken();
  }
}
