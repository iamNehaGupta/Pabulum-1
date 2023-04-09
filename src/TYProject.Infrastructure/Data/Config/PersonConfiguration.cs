using Ardalis.Specification;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TYProject.Core.PersonAggregate;

namespace TYProject.Infrastructure.Data.Config;
internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
  public void Configure(EntityTypeBuilder<Person> entity)
  {
    entity.ToTable("Person");

    entity.Property(e => e.CreatedOnDate).HasColumnType("datetime");
    entity.Property(e => e.Dob)
    .HasColumnType("datetime")
        .HasColumnName("DOB");
    entity.Property(e => e.FirstName)
    .IsRequired()
        .HasMaxLength(200);
    entity.Property(e => e.Gender)
        .IsRequired()
        .HasMaxLength(1)
        .HasColumnName("GENDER");
    entity.Property(e => e.LastModifiedOnDate).HasColumnType("datetime");
    entity.Property(e => e.LastName).HasMaxLength(200);
    entity.Property(e => e.MiddleName).HasMaxLength(200);
    entity.Property(e => e.RowVer)
        .IsRequired()
    .IsRowVersion()
        .IsConcurrencyToken();
    entity.Property(e => e.Type)
    .IsRequired()
    .HasMaxLength(1);

    entity.HasOne(d => d.Company).WithMany(p => p.Person)
        .HasForeignKey(d => d.CompanyId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Person_Company_CreatedBy");

    entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PersonCreatedByNavigation)
        .HasForeignKey(d => d.CreatedBy)
        .OnDelete(DeleteBehavior.ClientSetNull);

    entity.HasOne(d => d.LastModifiedByNavigation).WithMany(p => p.PersonLastModifiedByNavigation).HasForeignKey(d => d.LastModifiedBy);

    
}
}
