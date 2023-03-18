// using Microsoft.EntityFrameworkCore;

// using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql;
using WebAppl.DbRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddDbContext<WebApiDbContext>(dbContextOptions => dbContextOptions
//     .UseMySql("server=localhost;user=root;password=1234;database=ef", new Version(8, 0, 31))
//     // The following three options help with debugging, but should
//     // be changed or removed for production.
//     .LogTo(Console.WriteLine, LogLevel.Information)
//     .EnableSensitiveDataLogging()
//     .EnableDetailedErrors());
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebApiContext>(_ => 
    _.UseMySql(connectionString, 
        new MySqlServerVersion(new Version(8, 0, 31)))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
    );
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddCors(c => {
//     c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// });

builder.Services.AddControllersWithViews().AddNewtonsoftJson(
     options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

var app = builder.Build();

// app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();