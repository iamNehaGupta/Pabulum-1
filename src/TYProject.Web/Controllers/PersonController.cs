using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TYProject.Core.CompanyAggregate;
using TYProject.Core.PersonAggregate;
using TYProject.Infrastructure.Data;
using TYProject.Web.Extensions;

namespace TYProject.Web.Controllers;
public class PersonController : Controller
{
  private const string InternalServerErrorMessage = "Due to some technical difficulty we are unable to process your request. Please contact support";

  public readonly AppDbContext context;
  private readonly Serilog.ILogger _logger;

  public PersonController(AppDbContext context, Serilog.ILogger logger)
  {
    this.context = context;
    this._logger = logger;
  }

  public IActionResult Index()
  {
    return View();
  }

  public IActionResult Employees()
  {
    return View();
  }

  [HttpGet]
  [AllowAnonymous]
  public IActionResult Create(long companyId)
  {
    var vm = new PersonViewModel(0, companyId, string.Empty, null, null, DateTime.Now, Gender.Male, false, Array.Empty<byte>());
    return View(vm);
  }

  [HttpPost]
  [AllowAnonymous]
  [ValidateAntiForgeryToken]
  public async  Task<IActionResult> Create(PersonViewModel person)
  {
    if (!ModelState.IsValid)
    {
     
      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }

    var model = new Person(person.Id, person.Companyid, person.FirstName, person.LastName, person.MiddleName, Core.PersonAggregate.Type.Employee, person.DOB, person.Gender, person.IsDeleted, 1, DateTime.Now, null, null, Array.Empty<byte>());
    var company = await context.People.AddAsync(model);

    await context.SaveChangesAsync();
    return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  }

  [HttpGet]
  public IActionResult Success()
  { 
      return PartialView();
  }

  [HttpPost] 
  [ValidateAntiForgeryToken]
  public IActionResult Success(PersonViewModel person)
  {
    return View();
  
  }

  //[HttpGet]
  //public IActionResult Create()
  //{
  //  return View();
  //}

 // [HttpPost]
  //[ValidateAntiForgeryToken]
  //public async Task<IActionResult> Create1(PersonViewModel payload)
 // {
  // logger.Information("Received create company request: @request", payload);
//
//    if (!ModelState.IsValid)
//    {
//      logger.Information("payload contains invalid data", ModelState.GetErrors());

//      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
 //   }
  //  try
  //  {
//      var model = new Person(0, payload.Id, payload.Companyid, payload.FirstName, payload.LastName, payload.MiddleName, payload.DOB, payload.Gender, payload.IsDeleted, payload.RowVer, Array.Empty<byte>());
//      var persons = context.Companies.AddAsync(model);
//      await context.SaveChangesAsync();
//      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
//    }
//    catch (Exception ex)
//    {
//      logger.Error(ex, "Failed to create person");
 //     return Json(InternalServerError());
 //   }
 // }

  //[HttpGet]
  //public async Task<IActionResult> Edit(long id)
  //{
  //  try
  //  {
  //    var person = await GetById(id);

  //    if (person == null)
  //    {
  //      logger.Information("Failed to find person");
  //      return View("AccessDenied");
  //    }
  //    var vm = new PersonViewModel(person.Id, person.Firstname, person.Lastname, person.Middlename, person.DOB, person.Gender, person.IsDeleted, person.RowVer);
  //    return View(vm);
  //  }
  //  catch (Exception ex)
  //  {
  //    logger.Error(ex, "Failed to person");
  //    return View("Error");
  //  }
  //}

  //[HttpPost]
  //[ValidateAntiForgeryToken]
  //public async Task<IActionResult> Edit(PersonViewModel payload)
  //{
  //  logger.Information("Received modify person request: @request", payload);

  //  if (!ModelState.IsValid)
  //  {
  //    logger.Information("payload contains invalid data", ModelState.GetErrors());

  //    return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  //  }
  //  try
  //  {
  //    var person = await GetById(payload.Id);

  //    if (person is null)
  //    {
  //      logger.Information("Failed to find person with id: ", payload.Id);
  //      ModelState.AddModelError(string.Empty, "Person doesn't exist");

  //      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  //    }
  //    if (!person.RowVer.Equals(payload.RowVer))
  //    {
  //      logger.Information("person is not matched: ", payload.Id);
  //      ModelState.AddModelError(string.Empty, "Person is not matched");

  //      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  //    }

  //    person.Firstname = payload.FirstName;
  //    person.Middlename = payload.MiddleName;
  //    person.Lastname = payload.LastName;
  //    person.DOB = payload.DOB;
  //    person.Gender = payload.Gender;
  //    person.IsDeleted = payload.IsDeleted;
  //    person.LastModifiedBy = 1;
  //    person.LastModifiedOnDate = DateTime.Now;

  //    await context.SaveChangesAsync();

  //    return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  //  }
  //  catch (Exception ex)
  //  {
  //    logger.Error(ex, "Failed to modify person");

  //    return Json(InternalServerError());
  //  }
  //}

  //[HttpPost]
  //public async Task<IActionResult> ActivateDeactivate(PersonViewModel payload)
  //{
  //  logger.Information("Received activate/deactivate person request: @request", payload);
  //  if (!ModelState.IsValid)
  //  {
  //    logger.Information("payload contains invalid data", ModelState.GetErrors());

  //    return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  //  }
  //  try
  //  {
  //    var person = await GetById(payload.Id);

  //    if (person is null)
  //    {
  //      logger.Information("Failed to find Person with id: ", payload.Id);
  //      ModelState.AddModelError(string.Empty, "Person doesn't exist");

  //      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  //    }
  //    if (!person.RowVer.Equals(payload.RowVer))
  //    {
  //      logger.Information("Person is not matched: ", payload.Id);
  //      ModelState.AddModelError(string.Empty, "Person is not matched");

  //      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  //    }
  //    person.IsDeleted = payload.IsDeleted;
  //    person.LastModifiedBy = 1;
  //    person.LastModifiedOnDate = DateTime.Now;

  //    await context.SaveChangesAsync();

  //    return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
  //  }
  //  catch (Exception ex)
  //  {
  //    logger.Error(ex, "Failed to activate/deactivate person");

  //    return Json(InternalServerError());
  //  }
  //}

  public async Task<JsonResult> AllPeople(long companyId)
  {
    try
    {
      var people = new List<PersonViewModel>();

      var company = await context.Companies
        .Include(x => x.People)
        .Where(x => x.Id == companyId)
        .FirstOrDefaultAsync();

      if (company == null)
      {
        return Json(people);
      }

      people = company.People
        .Select(x => new PersonViewModel(x.Id, x.CompanyId, x.Firstname, x.Lastname, x.Middlename , x.DOB, x.Gender, x.IsDeleted, x.RowVer))
        .ToList();

      return Json(new OperationResult<IList<PersonViewModel>>(ModelState.IsValid, ModelState.GetErrors())
      {
        Data = people
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

  public class PersonBasicInfoModel
  {
    public PersonBasicInfoModel(long id, long companyid, string firstName, string lastName, string middleName, DateTime dob, Gender gender, bool isDeleted, byte[] rowVer)
    {
      Id = id;
      Companyid = companyid;
      FirstName = firstName;
      LastName = lastName;
      MiddleName = middleName;
      DOB = dob;
      Gender = gender;
      IsDeleted = isDeleted;
      RowVer = rowVer;

    }
    public long Id { get; set; }
    public long Companyid { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DOB { get; set; }
    public Gender Gender { get; set; }
    public bool IsDeleted { get; set; }
    public byte[] RowVer { get; set; }
  }

  public class PersonViewModel
  {

    public PersonViewModel()
    {
      Id = 0;
      Companyid = 0;
      FirstName = string.Empty;
      DOB = DateTime.Now;
      Gender = Gender.Male;
      IsDeleted = false;
      RowVer = Array.Empty<byte>();
    }
    public PersonViewModel(long id, long companyid, string firstName, string? lastName, string? middleName, DateTime dob, Gender gender, bool isDeleted, byte[] rowVer)
    {
      Id = id;
      Companyid = companyid;
      FirstName = firstName;
      LastName = lastName;
      MiddleName = middleName;
      DOB = dob;
      Gender = gender;
      IsDeleted = isDeleted;
      RowVer = rowVer;

    }
    public long Id { get; set; }
    public long Companyid { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Please enter date of birth")]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; }
    public Gender Gender { get; set; }
    public bool IsDeleted { get; set; }
    public byte[] RowVer { get; set; }
  }


  #region Helpers

  private async Task<Person?> GetById(long id)
  {
    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

    return person;
  }
  #endregion

}






