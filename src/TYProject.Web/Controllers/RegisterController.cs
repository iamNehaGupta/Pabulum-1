using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TYProject.Core.RegisterAggregate;
using TYProject.Infrastructure.Data;
using TYProject.Web.Extensions;

namespace TYProject.Web.Controllers;
public class RegisterController : Controller
{

  private const string InternalServerErrorMessage = "Due to some technical difficulty we are unable to process your request. Please contact support";

  public readonly AppDbContext context;
  private readonly Serilog.ILogger _logger;

  public RegisterController(AppDbContext context, Serilog.ILogger logger)
  {
    this.context = context;
    this._logger = logger;
  }


  public IActionResult RegisterIndex()
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
  public async Task<IActionResult> create(RegisterViewModel register)
  {
    if (!ModelState.IsValid)
    {

      return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));
    }

    var model = new Register(register.Name, register.Email, register.Password, register.ReTypePassword);

    await context.SaveChangesAsync();

    return Json(new OperationResult(ModelState.IsValid, ModelState.GetErrors()));

  }

  public class RegisterViewModel
  {
    public RegisterViewModel()
    {
      Name = string.Empty;
      Email = string.Empty; 
      Password = string.Empty;  
      ReTypePassword = string.Empty;    

    }

      public RegisterViewModel(string name, string email, string password, string retypepassword)
      {
        Name = name;
        Email = email;
        Password = password;
        ReTypePassword = retypepassword;
      }

    [Required(ErrorMessage ="Please Enter Your name")]
      public string Name { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
      public string ReTypePassword { get; set; }
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

