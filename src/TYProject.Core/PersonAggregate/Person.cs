using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using TYProject.Core.CompanyAggregate;
using TYProject.SharedKernel;
using TYProject.SharedKernel.Interfaces;

namespace TYProject.Core.PersonAggregate;
public class Person : EntityBase<long>, IAggregateRoot
{
  public Person(long id, long companyid, string firstname, string? lastname, string? middlename, DateTime dob, Gender gender, bool isDeleted, long createdBy, DateTime createdOnDate, long? lastModifiedBy, DateTime? lastModifiedOnDate, byte[] rowVer, Company company)
    : base(id)
  {
    Companyid = companyid;
    Firstname = Guard.Against.NullOrWhiteSpace(firstname, nameof(firstname));
    Lastname = lastname;
    Middlename = middlename;
    DOB = dob;
    Gender = gender;
    IsDeleted = isDeleted;
    CreatedBy = createdBy;
    CreatedOnDate = createdOnDate;
    LastModifiedBy = lastModifiedBy;
    LastModifiedOnDate = lastModifiedOnDate;
    RowVer = Guard.Against.Null(rowVer, nameof(rowVer));

    Company = company;

  }
  public long Companyid { get; set; }
  public string Firstname { get; set; }
  public string? Lastname { get; set; }

  public string? Middlename { get; set; }
  public DateTime DOB { get; set; } = DateTime.Now;

  public Gender Gender { get; set; }

  public bool IsDeleted { get; set; }


  public long CreatedBy { get; set; }
  public DateTime CreatedOnDate { get; set; }
  public long? LastModifiedBy { get; set; }
  public DateTime? LastModifiedOnDate { get; set; }

  [Timestamp]
  public byte[] RowVer { get; set; }

  public Company Company { get; set; }

}


public enum Gender
{
  Male,
  Female,
  Other
}
