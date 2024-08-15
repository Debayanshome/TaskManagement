using Ardalis.Specification;
using System.Linq.Expressions;
using TaskManagement.Repository.Common;
using TaskManagement.Shared.Repository;

namespace TaskManagement.Repository.Specifications.Task
{
    public class PaginatedTaskSpecification : Specification<Models.TaskDetail>
    {
        private readonly SearchExpressionBuilder<Models.TaskDetail> ExpressionBuilder;
        public readonly PageQueryModel PageQueryModel;
        public readonly List<string> StatusFilter;

        public PaginatedTaskSpecification(PageQueryModel pageQueryModel, List<string> statusFilter)
        {
            this.PageQueryModel = pageQueryModel;
            this.StatusFilter = statusFilter;
            AddQuery();
        }

        private void AddQuery()
        {
            Query
                .Include(x => x.Employee)
                .Include(x => x.Documents);
            if (!string.IsNullOrWhiteSpace(PageQueryModel.Search))
            {
                Query.Where(x => (x.Employee != null && x.Employee.Name != null && x.Employee.Name.ToUpper().Contains(PageQueryModel.Search.ToUpper())) ||
                (x.Documents != null && x.Documents.FirstOrDefault() != null && !string.IsNullOrWhiteSpace(x.Documents.FirstOrDefault().Name) && x.Documents.FirstOrDefault().Name.ToUpper().Contains(PageQueryModel.Search.ToUpper())) ||
                (x.Status != null && x.Status.ToUpper().Contains(PageQueryModel.Search.ToUpper()))||
                (x.Name != null && x.Name.ToUpper().Contains(PageQueryModel.Search.ToUpper())));
            }

            if (StatusFilter != null && StatusFilter.Count > 0)
            {

                Query.Where(ExpressionBuilder.BuildOrSearchExpression(StatusFilter.Select(c => c.ToUpper()).Select(s => (Expression<Func<Models.TaskDetail, bool>>)(p => p != null && p.Status != null &&
                 p.Status.ToUpper().Contains(s))).ToList()));
            }
            if (!string.IsNullOrWhiteSpace(PageQueryModel.SortCol) && !string.IsNullOrWhiteSpace(PageQueryModel.SortOrder))
            {
                switch (PageQueryModel.SortCol.ToLower())
                {
                    case "name":
                        ApplySort(x => x.Name);
                        break;
                    case "employeename":
                        ApplySort(x => x.Employee.Name);
                        break;
                    case "documentname":
                        ApplySort(x => x.Documents.FirstOrDefault().Name);
                        break;
                    case "status":
                        ApplySort(x => x.Status);
                        break;
                }
            }
            else
            {
                Query.OrderByDescending(x => x.CreatedOn);
            }
        }

        private void ApplySort(Expression<Func<Models.TaskDetail, object>> expression)
        {
            if (PageQueryModel.SortOrder.ToLower().Equals("asc"))
            {
                Query.OrderBy(expression);
            }
            else
            {
                Query.OrderByDescending(expression);
            }
        }
    }
}
