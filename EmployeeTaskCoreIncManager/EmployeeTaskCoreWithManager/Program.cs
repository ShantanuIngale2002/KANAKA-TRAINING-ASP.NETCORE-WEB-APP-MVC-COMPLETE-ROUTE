using CoreTaskEmployee.Repository;
using CoreTaskEmployee.Repository.EFCore;
using CoreTaskEmployee.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




builder.Services.AddSession();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetService<IConfiguration>();
builder.Services.AddDbContext<EmployeeDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnectionString") ?? throw new InvalidOperationException("Connection String not Found!!")
        )
    );

builder.Services.AddScoped<IEmployeeRepository, EmployeeService>();




var app = builder.Build();




app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
