using Ardalis.Specification;

namespace TaskManagement.Repository.Specifications.Docment
{
    public sealed class DocumentFetchSpecification : Specification<Models.Document>
    {
        public DocumentFetchSpecification(List<int> documentId)
        {
            Query
              .Where(b => documentId.Contains(b.Id));
        }
    }
}
