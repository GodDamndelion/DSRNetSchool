using DSRNetSchool.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppLogger();

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAppHealthChecks();
app.UseAppSwagger();
//app.UseAppCors(); //���� �� ������...

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
