

/*
 * Stuffs added in file
    1. Dependency Injection 1 - add dependency and create a object to be used internally as a service using addscoped which we apply to class instance in constructor in service class not in the interface ie. in Repository/ProductService.cs
        builder.Services.AddScoped<IProductService, ProductService>();
    2. Dependency Injection 2 - adding connection string
            builder.Services.AddDbContext<ProductContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DatabaseString")??throw new InvalidOperationException("Connection String not Found!!")
                    )
                );
        #Important Note :- Here we get connection string from appsettings.json
    
    3. Swagger to check api data
        rightC -> Solution -> manage nugget -> swash.buckle.aspnetcore 
            builder.Servies.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        also before app.Run() at the end add
            app.UseSwagger();
            app.UseSwaggerUI();
 */



using GenericRepoPatternCORE.Data;
using GenericRepoPatternCORE.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// dependency injection 2, as a servie for dbContext file
builder.Services.AddDbContext<ProductContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnectionString")??throw new InvalidOperationException("Connection String not Found!!")
        )
    );

// added swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// dependency injection 1, as a service to Repo class file
builder.Services.AddScoped<IProductService, ProductService>();

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

// swagger need
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
