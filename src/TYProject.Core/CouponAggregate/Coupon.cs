using TYProject.SharedKernel;
using TYProject.SharedKernel.Interfaces;
using TYProject.Core.CompanyAggregate;
using TYProject.Core.PersonAggregate;

#nullable disable

namespace TYProject.Core.CouponAggregate;


public partial class Coupon: EntityBase<long>, IAggregateRoot
{
  public long PersonId { get; set; }

  public long CompanyId { get; set; }

  public string Code { get; set; }

  public DateTime? ExpiryDate { get; set; }

  public DateTime PurchasedOn { get; set; }

  public DateTime? UtilizedOn { get; set; }

  public virtual Company Company { get; set; }

  public virtual Person Person { get; set; }
}
