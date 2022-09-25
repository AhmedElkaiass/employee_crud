
namespace  EmployeeCRUD.Core.Services.Contracts.Genaric
{

    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
