using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TYProject.Web.Extensions;

public static class ModelStateExtensions
{

  public static IList<string> GetErrors(this ModelStateDictionary modelstate)
  {
    var errors = modelstate.Values
      .SelectMany(x => x.Errors)
      .Select(x => x.ErrorMessage).ToList();

    return errors;
  }
}
