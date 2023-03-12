using Ardalis.Result;

namespace TYProject.Core.Interfaces;

public interface IDeleteContributorService
{
    public Task<Result> DeleteContributor(int contributorId);
}
