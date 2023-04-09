﻿using TYProject.SharedKernel.Interfaces;
using TYProject.SharedKernel;
using TYProject.Core.PersonAggregate;
using TYProject.Core.LoginAggregate;
using TYProject.Core.CouponAggregate;

#nullable disable

namespace TYProject.Core.CompanyAggregate;
public partial class Company: EntityBase<long>, IAggregateRoot
{ 

  public string Name { get; set; }

  public string Description { get; set; }

  public bool IsDeleted { get; set; }

  public long CreatedBy { get; set; }

  public DateTime CreatedOnDate { get; set; }

  public long? LastModifiedBy { get; set; }

  public DateTime? LastModifiedOnDate { get; set; }

  public byte[] RowVer { get; set; }

  public virtual ICollection<Coupon> Coupons { get; } = new List<Coupon>();

  public virtual Login CreatedByNavigation { get; set; }

  public virtual Login LastModifiedByNavigation { get; set; }

  public virtual ICollection<Person> People { get; } = new List<Person>();
}
