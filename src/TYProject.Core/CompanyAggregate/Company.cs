using TYProject.SharedKernel.Interfaces;
using TYProject.SharedKernel;
using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;
using TYProject.Core.PersonAggregate;

namespace TYProject.Core.CompanyAggregate;
public class Company : EntityBase<long>, IAggregateRoot
{
  public Company(long id, string name, string? description, bool isDeleted, long createdBy, DateTime createdOnDate, long? lastModifiedBy, DateTime? lastModifiedOnDate, byte[] rowVer, IList<Person> people)
    :base(id)
  {
    Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    Description = description;
    IsDeleted = isDeleted;
    CreatedBy = createdBy;
    CreatedOnDate = createdOnDate;
    LastModifiedBy = lastModifiedBy;
    LastModifiedOnDate = lastModifiedOnDate;
    RowVer = Guard.Against.Null(rowVer, nameof(rowVer));

    People = people;
  }
  public string Name { get; set; }
  public string? Description { get; set; }
  public bool IsDeleted { get; set; }

  public long CreatedBy { get; set; }
  public DateTime CreatedOnDate { get; set; }
  public long? LastModifiedBy { get; set; }
  public DateTime? LastModifiedOnDate { get; set; }

  [Timestamp]
  public byte[] RowVer { get; set; }

  public IList<Person> People { get; set; } = new List<Person>();
}
