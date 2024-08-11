using Ardalis.Specification;

namespace TaskManagement.Shared.Repository.Interface
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
