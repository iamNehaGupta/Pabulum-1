using Microsoft.AspNetCore.Mvc;
using TYProject.Infrastructure.Data;
using TYProject.Web.Extensions;

namespace TYProject.Web.Controllers;
public class CompanyController : Controller
{
  private const string InternalServerErrorMessage = "Due to some technical difficulty we are unable to process your request. Please contact support";

  public readonly AppDbContext context;
  public CompanyController(AppDbContext context)
  {
    this.context = context;
  }
  public IActionResult Index()
  {
    return View();
  }

  [HttpGet]
  public IActionResult Create()
  {
    return View();
  }

  public JsonResult AllCompanies()
  {
    try
    {
      var companies = context.Companies
        .Select(x => new CompanyViewModel(x.Id, x.Name, x.IsDeleted))
        .ToList();

      return Json(new OperationResult<IList<CompanyViewModel>>(ModelState.IsValid, ModelState.GetErrors())
      {
        Data = companies
      });
    }
    catch (Exception)
    {
      return Json(InternalServerError());
    }
  }

  private OperationResult InternalServerError(string message = InternalServerErrorMessage)
  {

    ModelState.AddModelError("InternalServerError", message);
    return new OperationResult(ModelState.IsValid, ModelState.GetErrors());
  }

  //[HttpPost]
  //[ValidateAntiForgeryToken]
  //public IActionResult Create()
  //{
  //  return Json(new OperationResult(true, new List<string>()));
  //}

  public class OperationResult<T>
  {
    public OperationResult(bool isSuccess, IList<string> errors)
    {
      this.IsSuccess = isSuccess;
      this.Errors = errors;
    }

    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public IList<string> Errors { get; set; }
  }

  public class OperationResult
  {
    public OperationResult(bool isSuccess, IList<string> errors)
    {
      this.IsSuccess = isSuccess;
      this.Errors = errors;
    }

    public bool IsSuccess { get; set; }
    public IList<string> Errors { get; set; }
  }

  public class CompanyViewModel
  {
    public CompanyViewModel(long id, string name, bool isActive) 
    {
      Id = id;
      Name = name;
      IsActive = isActive;
    }
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
  }
}
