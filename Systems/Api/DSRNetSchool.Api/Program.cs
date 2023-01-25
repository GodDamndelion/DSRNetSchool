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

services.AddHttpContextAccessor(); //Для CorrelationId
services.AddAppCors();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger(identitySettings, swaggerSettings);
services.AddAppAutoMappers();

services.AddAppControllerAndViews();

services.RegisterAppServices();

//builder.Services.AddControllers(); //Стандартный, нам не нужен

var app = builder.Build();

app.UseAppHealthChecks();
app.UseAppSwagger();
//app.UseAppCors(); //Пока не нужно

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);

// Configure the HTTP request pipeline.

//app.UseAuthorization();
//app.MapControllers();

app.UseStaticFiles(); //Сам дописал, чинит вёрстку

app.UseAppControllerAndViews();

app.UseAppMiddlewares();

app.Run();
