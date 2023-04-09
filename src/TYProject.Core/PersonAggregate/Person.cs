using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using TYProject.Core.CompanyAggregate;
using TYProject.Core.CouponAggregate;
using TYProject.Core.LoginAggregate;
using TYProject.SharedKernel;
using TYProject.SharedKernel.Interfaces;

#nullable disable

namespace TYProject.Core.PersonAggregate;

public partial class Person : EntityBase<long>, IAggregateRoot
{
  public long CompanyId { get; set; }

  public string FirstName { get; set; }

  public string LastName { get; set; }

  public string MiddleName { get; set; }

  public DateTime DOB { get; set; }

  public string GENDER { get; set; }

  public string Type { get; set; }

  public bool IsDeleted { get; set; }

  public long CreatedBy { get; set; }

  public DateTime CreatedOnDate { get; set; }

  public long? LastModifiedBy { get; set; }

  public DateTime? LastModifiedOnDate { get; set; }

  public byte[] RowVer { get; set; }

  public virtual Company Company { get; set; }

  public virtual ICollection<Coupon> Coupons { get; } = new List<Coupon>();

  public virtual Login CreatedByNavigation { get; set; }

  public virtual Login LastModifiedByNavigation { get; set; }
}

public enum Gender
{
  Male,
  Female,
  Other
}

public enum Type
{
  Employee,
  Admin,
  Manager,
}
