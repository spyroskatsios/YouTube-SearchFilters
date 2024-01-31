using System.Reflection;
using Customers.Common;
using Customers.Persistence;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDb"));
    options.UseExceptionProcessor();
});

builder.Services.AddScoped<IAppDbContext, AppDbContext>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();