using DSRNetSchool.Api;
using DSRNetSchool.Api.Configuration;
using DSRNetSchool.Services.Settings;
using DSRNetSchool.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

builder.AddAppLogger();

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger(mainSettings, swaggerSettings);

services.AddAppControllerAndViews();

services.RegisterAppServices();

//builder.Services.AddControllers(); //Стандартный, нам не нужен

var app = builder.Build();

app.UseAppHealthChecks();
app.UseAppSwagger();
//app.UseAppCors(); //пока не сделал...

// Configure the HTTP request pipeline.

//app.UseAuthorization();
//app.MapControllers();

app.UseStaticFiles(); //Сам дописал, чинит вёрстку

app.UseAppControllerAndViews();

app.Run();
