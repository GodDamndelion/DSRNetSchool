using DSRNetSchool.Api;
using DSRNetSchool.Api.Configuration;
using DSRNetSchool.Context;
using DSRNetSchool.Services.Settings;
using DSRNetSchool.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mainSettings = Settings.Load<MainSettings>("Main");
var identitySettings = Settings.Load<IdentitySettings>("Identity");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

builder.AddAppLogger();

// Configure services

var services = builder.Services;

services.AddHttpContextAccessor(); //��� CorrelationId
services.AddAppCors();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger(identitySettings, swaggerSettings);
services.AddAppAutoMappers();

services.AddAppControllerAndViews();

services.RegisterAppServices();

//builder.Services.AddControllers(); //�����������, ��� �� �����

var app = builder.Build();

app.UseAppHealthChecks();
app.UseAppSwagger();
//app.UseAppCors(); //���� �� �����

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);

// Configure the HTTP request pipeline.

//app.UseAuthorization();
//app.MapControllers();

app.UseStaticFiles(); //��� �������, ����� ������

app.UseAppControllerAndViews();

app.UseAppMiddlewares();

app.Run();
