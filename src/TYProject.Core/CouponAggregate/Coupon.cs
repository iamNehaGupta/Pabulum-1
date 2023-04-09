using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYProject.SharedKernel;
using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;
using TYProject.SharedKernel.Interfaces;
using TYProject.Core.CompanyAggregate;
using TYProject.Core.PersonAggregate;

#nullable disable

namespace TYProject.Core.CouponAggregate;


public partial class Coupon: EntityBase<long>, IAggregateRoot
{
  public long Code { get; set; }


  public long PersonId { get; set; }

  public long CompanyId { get; set; }

  public DateTime? ExpiryDate { get; set; }

  public DateTime PurchasedOn { get; set; }

  public DateTime? UtilizedOn { get; set; }

  public virtual Company Company { get; set; }

  public virtual Person Person { get; set; }
}
