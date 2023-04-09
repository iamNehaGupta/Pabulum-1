using Microsoft.AspNetCore.Mvc;
using TYProject.Core.CouponAggregate;

namespace TYProject.Web.Controllers;
public class CouponController : Controller
{
  public IActionResult Index()
  {
    return View();
  }

  public IActionResult CouponForPerson(long companyId, long personId)
  {
    var code = Guid.NewGuid().ToString();
    var coupon = new Coupon(code, 0, personId, companyId, null, DateTime.Now, null);
  }
}
