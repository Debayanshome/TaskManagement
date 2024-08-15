using System.Threading;
using TaskManagement.Repository.Models;
using TaskManagement.Repository.Specifications.Employee;
using TaskManagement.Shared.Repository.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagement.Core.Task.Commands.Shared
{
    public abstract class TaskBaseHandler
    {
        private readonly IRepository<Repository.Models.Employee> _employeeRepository;
        protected TaskBaseHandler(IRepository<Repository.Models.Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        protected async System.Threading.Tasks.Task AssignValuesAsync(TaskBaseCommandModel request, Repository.Models.TaskDetail taskDetail, CancellationToken cancellationToken)
        {
            taskDetail.Name = request.Name;
            taskDetail.Details = request.Details;
            taskDetail.Notes = request.Notes;
            taskDetail.StartDate = request.StartDate.ToUniversalTime();
            taskDetail.DueDate = request.DueDate.ToUniversalTime();
            taskDetail.Timezone = request.Timezone;
            await TaskEmployeeDataAsync(request.Employee, taskDetail, cancellationToken);
            TaskDocumentListData(request.Documents, taskDetail);
        }
        protected async System.Threading.Tasks.Task TaskEmployeeDataAsync(EmployeeCommandModel requestEmployee, Repository.Models.TaskDetail taskDetails, CancellationToken cancellationToken)
        {
            if(taskDetails.Employee != null && requestEmployee.Id.HasValue)
            {
                if (requestEmployee.Id.Value == taskDetails.Employee.Id)
                    taskDetails.Employee.Name = requestEmployee.Name;
                else
                {
                    taskDetails.Employee.IsDeleted = true;

                    await _employeeRepository.UpdateAsync(taskDetails.Employee, cancellationToken);
                    var employeeDetailSaved = await _employeeRepository.SaveChangesAsync(cancellationToken);

                    var employeeDetail = await this._employeeRepository.FirstOrDefaultAsync(new EmployeeFetchSpecification(requestEmployee.Id.Value), cancellationToken);
                    employeeDetail.Name = requestEmployee.Name;
                    taskDetails.Employee = employeeDetail; 
                }                    
            }
            else
            {
                if (taskDetails.Employee != null)
                    taskDetails.Employee.IsDeleted = true;
                taskDetails.Employee = new Repository.Models.Employee { Name = requestEmployee.Name, IsDeleted = false };
            }
        }

        protected void TaskDocumentListData(List<DocumentCommandModel> requestDocuments, Repository.Models.TaskDetail taskDetails)
        {
            UpdateDeletedDocumentData(requestDocuments, taskDetails);
            if (taskDetails.Documents == null)
            {
                taskDetails.Documents = new List<Document>();
            }

            if (requestDocuments != null && requestDocuments.Count > 0)
            {
                taskDetails.Documents.ForEach(document =>
                {
                    var requestedDocument = requestDocuments.FirstOrDefault(x => x.Id != null && x.Id.HasValue && x.Id == document.Id);
                    if (requestedDocument != null)
                    {
                        document.Name = requestedDocument.Name;
                        document.Path = requestedDocument.Path;
                        document.Type = requestedDocument.Type;
                    }
                    else
                    {
                        document.IsDeleted = true;
                    }
                });

                var newDocuments = requestDocuments.Where(x => !x.Id.HasValue);
                if (newDocuments.Count() > 0)
                {
                    taskDetails.Documents.AddRange(newDocuments.Select(c => new Document
                    {
                        Name = c.Name,
                        Path = c.Path,
                        Type = c.Type,
                        IsDeleted = false
                    }));
                }
            }
        }
        protected void UpdateDeletedDocumentData(List<DocumentCommandModel> requestDocuments, Repository.Models.TaskDetail taskDetails)
        {
            if (requestDocuments == null && taskDetails.Documents != null && taskDetails.Documents.Count > 0)
            {
                taskDetails.Documents.ForEach(document =>
                {
                    document.IsDeleted = true;
                });
            }
        }
    }
}
