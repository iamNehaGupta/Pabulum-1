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
    entity.Property(e => e.DOB).HasColumnType("datetime");
    entity.Property(e => e.FirstName)
        .IsRequired()
        .HasMaxLength(200);
    entity.Property(e => e.GENDER)
        .IsRequired()
        .HasMaxLength(1);
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

    entity.HasOne(d => d.Company).WithMany(p => p.People)
        .HasForeignKey(d => d.CompanyId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Person_Company_CreatedBy");

    entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PersonCreatedByNavigations)
        .HasForeignKey(d => d.CreatedBy)
        .OnDelete(DeleteBehavior.ClientSetNull);

    entity.HasOne(d => d.LastModifiedByNavigation).WithMany(p => p.PersonLastModifiedByNavigations).HasForeignKey(d => d.LastModifiedBy);
}
}
