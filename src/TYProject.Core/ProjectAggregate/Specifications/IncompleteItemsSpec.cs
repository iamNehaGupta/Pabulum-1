using Ardalis.Specification;

namespace TYProject.Core.ProjectAggregate.Specifications;

public class IncompleteItemsSpec : Specification<ToDoItem>
{
  public IncompleteItemsSpec()
  {
    Query.Where(item => !item.IsDone);
  }
}
