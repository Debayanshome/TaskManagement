using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Shared.Repository.Interface;

namespace TaskManagement.Core.Task.Queries.GetById
{
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IReadRepository<Repository.Models.Task> userReadRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly ITenantProvider _tenantProvider;

        public Handler(IReadRepository<Repository.Models.User> userReadRepository, IMapper mapper, ILogger<Handler> logger, ITenantProvider tenantProvider)
        {
            this.userReadRepository = userReadRepository;
            this.mapper = mapper;
            this.logger = logger;
            _tenantProvider = tenantProvider;
        }

        public Task<ValidationResult> Handle(QueryModel request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
