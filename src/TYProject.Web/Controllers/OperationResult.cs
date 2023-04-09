namespace TYProject.Web.Controllers;

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
