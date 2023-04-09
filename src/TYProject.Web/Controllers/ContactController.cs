using Microsoft.AspNetCore.Mvc;
using TYProject.Core.ContactAggregate;
using TYProject.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using TYProject.Infrastructure.Data;

namespace TYProject.Web.Controllers;
public class ContactController : Controller
{
    private const string InternalServerErrorMessage = "Due to some technical difficulty we are unable to process your request. Please contact support";

    public readonly AppDbContext context;
    private readonly Serilog.ILogger _logger;

    public ContactController(AppDbContext context, Serilog.ILogger logger)
    {
      this.context = context;
      this._logger = logger;
    }


  public IActionResult ContactIndex()
  {
    return View();
  }

  [HttpGet]
  [AllowAnonymous]
  public IActionResult create()
  {
    return View();
  }

  [HttpPost]
  [AllowAnonymous]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> create(ContactViewModel contact)
  {
    

    if (!ModelState.IsValid)
    {

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }

    try
    {
      var model = new Contact()
      {
        Name = contact.Name,
        Email = contact.Email,
        Description = contact.Description
      };
      var data = context.Contacts.AddAsync(model);

      await context.SaveChangesAsync();

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    catch (Exception ex) 
    {
      ModelState.AddModelError(string.Empty, ex.Message);
      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }
    
  }

  public class ContactViewModel
  {
    public ContactViewModel()
    {
      Name = string.Empty;
      Email = string.Empty;
      Description = string.Empty;
    }

    public ContactViewModel(string name, string email, string description)
    {
      Name = name;
      Email = email;
      Description = description;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
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
 
}
