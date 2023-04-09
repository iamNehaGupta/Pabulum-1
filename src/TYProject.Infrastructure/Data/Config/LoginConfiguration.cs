using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TYProject.Core.LoginAggregate;

namespace TYProject.Infrastructure.Data.Config;
internal class LoginConfiguration : IEntityTypeConfiguration<Login>
{
  public void Configure(EntityTypeBuilder<Login> entity)
  {
    entity.ToTable("Login");
    entity.Property(e => e.CreatedOnDate).HasColumnType("datetime");
    entity.Property(e => e.LastModifiedOnDate).HasColumnType("datetime");
    entity.Property(e => e.Password)
        .IsRequired()
        .HasMaxLength(250);
    entity.Property(e => e.RowVer)
        .IsRequired()
        .IsRowVersion()
        .IsConcurrencyToken();
    entity.Property(e => e.UserName)
        .IsRequired()
        .HasMaxLength(250);

    entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
        .HasForeignKey(d => d.CreatedBy)
        .OnDelete(DeleteBehavior.ClientSetNull);

    entity.HasOne(d => d.LastModifiedByNavigation).WithMany(p => p.InverseLastModifiedByNavigation).HasForeignKey(d => d.LastModifiedBy);

  }
}
