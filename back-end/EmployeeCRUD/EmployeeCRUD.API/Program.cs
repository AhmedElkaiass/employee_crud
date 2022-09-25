using EmployeeCRUD.API.Filter;
using EmployeeCRUD.Core.DTOs;
using EmployeeCRUD.Core.Services;
using EmployeeCRUD.Infrastructure;
using EmployeeCRUD.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddConfiguredSwagger();
builder.Services.ConfigureIdentity();
#region Authnetications and Authorization
var jwtSetting = new JwtSettings();
builder.Configuration.Bind("Jwt", jwtSetting);
builder.Services.Configure<JwtSettings>(jwtSettingsOptions => builder.Configuration.Bind("Jwt", jwtSettingsOptions));
builder.Services.AddConfiguredJwtAuthentication(jwtSetting);
builder.Services.AddConfiguredAuthorization();

#endregion
builder.Services.AddCoreRepository(builder.Configuration);
builder.Services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true);
                    });
                });
builder.Services.AddCoreServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseConfiguredSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app
   // .MigrateDatabase()
    .Run();
