using System.Text.Json.Serialization;
using TaskManagement.Api;
using TaskManagement.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//startup registration
builder.Services.AddCommonDependencies(builder.Configuration, typeof(TaskManagement.Core.StartupSetup), "TaskManagement API");
builder.Services.AddDbContext(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
