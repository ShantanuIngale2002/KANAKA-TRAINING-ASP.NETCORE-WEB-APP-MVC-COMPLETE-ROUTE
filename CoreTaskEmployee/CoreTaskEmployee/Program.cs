using CoreTaskEmployee.Repository;
using CoreTaskEmployee.Repository.EFCore;
using CoreTaskEmployee.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// added 2
builder.Services.AddDbContext<EmployeeDbContext>(
    options => options.UseSqlServer(
        // get this from appsettings.json after adding connection string into it.
        /*
         *  from allowed host as
         *  "AllowedHosts": "*",
          "ConnectionStrings": {
            "DbConnectionString": "Data Source=DESKTOP-OK5KH4G\\SQLEXPRESS;Initial Catalog=CoreTaskEmployeeDB;Integrated Security=True;Trust Server Certificate=True"
          }
         */
        builder.Configuration.GetConnectionString("DbConnectionString") ?? throw new InvalidOperationException("Connection String not Found!!")
        )
    );


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// added 1
builder.Services.AddScoped<IEmployeeRepository, EmployeeService>();




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