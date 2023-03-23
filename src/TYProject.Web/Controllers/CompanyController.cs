using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TYProject.Core.CompanyAggregate;
using TYProject.Core.PersonAggregate;
using TYProject.Infrastructure.Data;
using TYProject.Web.Extensions;

namespace TYProject.Web.Controllers;
public class CompanyController : Controller
{
  private const string InternalServerErrorMessage = "Due to some technical difficulty we are unable to process your request. Please contact support";

  public readonly AppDbContext context;
  private readonly Serilog.ILogger logger;
  public CompanyController(AppDbContext context, Serilog.ILogger logger)
  {
    this.context = context;
    this.logger = logger;
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

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(CompanyViewModel payload)
  {
    logger.Information("Received create company request: @request", payload);

    if (!ModelState.IsValid)
    {
      logger.Information("payload contains invalid data", ModelState.GetErrors());

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    try
    {
      var model = new Company(0, payload.Name, payload.Description, payload.IsDeleted, 1, DateTime.Now, null, null, Array.Empty<byte>(), new List<Person>());

      var company = context.Companies.AddAsync(model);

      await context.SaveChangesAsync();

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    catch (Exception ex)
    {
      logger.Error(ex, "Failed to create company");

      return Json(InternalServerError());
    }
  }

  [HttpGet]
  public async Task<IActionResult> Edit(long id)
  {
    try
    {
      var company = await GetById(id);

      if (company == null)
      {
        logger.Information("Failed to find company");
        return View("AccessDenied");
      }

      var vm = new CompanyViewModel(company.Id, company.Name, company.Description, company.IsDeleted, company.RowVer);
      return View(vm);
    }
    catch(Exception ex) 
    {
      logger.Error(ex, "Failed to company");
      return View("Error");
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(CompanyViewModel payload)
  {
    logger.Information("Received modify company request: @request", payload);

    if (!ModelState.IsValid)
    {
      logger.Information("payload contains invalid data", ModelState.GetErrors());

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    try
    {
      var company = await GetById(payload.Id);

      if(company is null)
      {
        logger.Information("Failed to find company with id: ", payload.Id);
        ModelState.AddModelError(string.Empty, "Company doesn't exist");

        return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
      }

      if (!company.RowVer.Equals(payload.RowVer))
      {
        logger.Information("company version is not matched: ", payload.Id);
        ModelState.AddModelError(string.Empty, "Company version is not matched");

        return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
      }

      company.Name = payload.Name;
      company.Description = payload.Description;
      company.IsDeleted = payload.IsDeleted;
      company.LastModifiedBy = 1;
      company.LastModifiedOnDate = DateTime.Now;

      await context.SaveChangesAsync();

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    catch (Exception ex)
    {
      logger.Error(ex, "Failed to modify company");

      return Json(InternalServerError());
    }
  }

  [HttpPost]
  public async Task<IActionResult> ActivateDeactivate(CompanyViewModel payload)
  {
    logger.Information("Received activate/deactivate company request: @request", payload);

    if (!ModelState.IsValid)
    {
      logger.Information("payload contains invalid data", ModelState.GetErrors());

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    try
    {
      var company = await GetById(payload.Id);

      if (company is null)
      {
        logger.Information("Failed to find company with id: ", payload.Id);
        ModelState.AddModelError(string.Empty, "Company doesn't exist");

        return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
      }

      if (!company.RowVer.Equals(payload.RowVer))
      {
        logger.Information("company version is not matched: ", payload.Id);
        ModelState.AddModelError(string.Empty, "Company version is not matched");

        return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
      }

      company.IsDeleted = payload.IsDeleted;
      company.LastModifiedBy = 1;
      company.LastModifiedOnDate = DateTime.Now;

      await context.SaveChangesAsync();

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    catch (Exception ex)
    {
      logger.Error(ex, "Failed to activate/deactivate company");

      return Json(InternalServerError());
    }
  }

  public JsonResult AllCompanies()
  {
    try
    {
      var companies = context.Companies
        .Select(x => new CompanyViewModel(x.Id, x.Name, !x.IsDeleted))
        .ToList();

      return Json(new OperationResult<IList<CompanyBasicInfoModel>>(ModelState.IsValid, ModelState.GetErrors())
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

  public class CompanyBasicInfoModel
  {
    public CompanyBasicInfoModel(long id, string name, bool isActive)
    {
      Id = id;
      Name = name;
      IsActive = isActive;
    }
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
  }

  public class CompanyViewModel
  {
    public CompanyViewModel(long id, string name, string? description, bool isDeleted, byte[] rowVer)
    {
      Id = id;
      Name = name;
      Description = description;
      IsDeleted = isDeleted;
      RowVer = rowVer;
    }
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public byte[] RowVer { get; set; }
  }


  #region Helpers

  private async Task<Company?> GetById(long id)
  {
    var company = await context.Companies.FirstOrDefaultAsync(x => x.Id == id);

    return company;
  }
  #endregion

}
