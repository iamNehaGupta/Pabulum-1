using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TYProject.Core.ContactAggregate;

namespace TYProject.Infrastructure.Data.Config;
internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
  public void Configure(EntityTypeBuilder<Contact> entity)
  {
    
      entity.HasNoKey();

      entity.Property(e => e.Description).HasMaxLength(1000);
      entity.Property(e => e.Email)
          .HasMaxLength(25)
          .IsUnicode(false);
      entity.Property(e => e.Id)
          .ValueGeneratedOnAdd()
          .HasColumnName("ID");
      entity.Property(e => e.Name)
          .HasMaxLength(25)
          .IsUnicode(false);
  }
}
