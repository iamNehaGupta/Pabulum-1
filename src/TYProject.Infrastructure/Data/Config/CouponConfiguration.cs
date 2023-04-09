using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TYProject.Core.CouponAggregate;

namespace TYProject.Infrastructure.Data.Config;
internal class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
  public void Configure(EntityTypeBuilder<Coupon> entity)
  {
    entity.ToTable("Coupon");

    entity.HasNoKey();

    entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
    entity.Property(e => e.Id)
        .ValueGeneratedOnAdd()
        .HasColumnName("ID");
    entity.Property(e => e.PurchasedOn).HasColumnType("datetime");
    entity.Property(e => e.UtilizedOn).HasColumnType("datetime");

    entity.HasOne(d => d.Company).WithMany()
        .HasForeignKey(d => d.CompanyId)
        .OnDelete(DeleteBehavior.ClientSetNull);

    entity.HasOne(d => d.Person).WithMany()
        .HasForeignKey(d => d.PersonId)
        .OnDelete(DeleteBehavior.ClientSetNull);
  }

}
