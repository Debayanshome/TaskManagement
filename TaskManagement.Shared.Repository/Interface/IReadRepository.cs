using Ardalis.Specification;

namespace TaskManagement.Shared.Repository.Interface
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
        bool Any(ISpecification<T> specification);
        List<T> List(ISpecification<T> specification);
        Task<PaginatedList<T>> GetPagninated(ISpecification<T> specification, PageQueryModel pageQueryModel);
    }
}
