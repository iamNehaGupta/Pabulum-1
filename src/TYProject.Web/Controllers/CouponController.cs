using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TYProject.Core.CouponAggregate;
using TYProject.Infrastructure.Data;
using TYProject.Web.Extensions;

namespace TYProject.Web.Controllers;
public class CouponController : Controller
{
  private const string InternalServerErrorMessage = "Due to some technical difficulty we are unable to process your request. Please contact support";

  public readonly AppDbContext context;
  private readonly Serilog.ILogger _logger;

  public CouponController(AppDbContext context, Serilog.ILogger logger)
  {
    this.context = context;
    this._logger = logger;
  }
  public IActionResult Index()
  {
    return View();
  }

  public async Task<IActionResult> CouponForPerson(long companyId, long personId)
  {
    try
    {
      var company = await context.Companies
  .Include(x => x.People)
  .Include(x => x.People.Select(x => x.Coupons))
  .Where(x => x.Id == personId)
  .FirstOrDefaultAsync();

      if (company == null)
      {
        ModelState.AddModelError(string.Empty, "Company doesn't exist");
        return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
      }

      var person = company.People.Where(x => x.Id == personId)
        .FirstOrDefault();

      if (person == null)
      {
        ModelState.AddModelError(string.Empty, "invalid person");
        return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
      }
      for (var i = 0; i <= 15; i++)
      {
        var code = Guid.NewGuid().ToString();

        var coupon = new Coupon
        {
          Code = code,
          CompanyId = companyId,
          PersonId = personId,
          PurchasedOn = DateTime.Now,
        };

        person.Coupons.Add(coupon);
      }

      await context.SaveChangesAsync();

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    catch (Exception) 
    {
      ModelState.AddModelError(string.Empty, InternalServerErrorMessage);
      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
  }
}
