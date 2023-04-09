namespace TYProject.Web.Models;

public class PageHeaderViewModel
{
  public PageHeaderViewModel(string title, string? area = "-")
  {
    Title = title;
    Area = area;
  }
  public string Title { get; set; }
  public string? Area { get; set; }
}
