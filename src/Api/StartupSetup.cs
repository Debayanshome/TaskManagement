using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;
using TaskManagement.Shared.Web.Filters;

namespace TaskManagement.Api
{
    public static class StartupSetup
    {
        public static void AddCommonDependencies(this IServiceCollection services, IConfiguration configuration, Type coreType, string swaggerTitle)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<CustomExceptionFilter>();
                    options.Filters.Add<CustomActionFilter>();
                    options.Filters.Add<CustomResultFilter>();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddEndpointsApiExplorer();
            AddSwagger(services, swaggerTitle);
            AddMediator(services, coreType);
            AddMapper(services, coreType);
            AddFluentValidtors(services, coreType);
            services.AddProblemDetails();
            services.AddOptions();
            services.AddHealthChecks();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    var originsArray = configuration.GetSection("AllowedCORSOrigins").Get<string[]>();
                    if (originsArray != null || originsArray.Length > 0)
                    {
                        policy.WithOrigins(string.Join(",", originsArray));
                    }

                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });
        }
        private static void AddSwagger(IServiceCollection services, string swaggerTitle)
        {
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = swaggerTitle,
                    Description = "An ASP.NET Core Web API for managing task management activity"
                });

            });
        }
        private static void AddMediator(IServiceCollection services, Type coreType)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(coreType));
        }

        private static void AddFluentValidtors(IServiceCollection services, Type coreType)
        {
            services.AddValidatorsFromAssemblyContaining(coreType);
            services.AddFluentValidationAutoValidation();
        }
        private static void AddMapper(IServiceCollection services, Type coreType)
        {
            services.AddAutoMapper(coreType);
        }
    }
}
