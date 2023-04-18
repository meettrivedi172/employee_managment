using WEBAPI.Data;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Interfaces.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddDbContext<DataContext>(opt =>
// {
//     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
// });

 builder.Services.AddCors();






// add for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// End  of swagger

// Configure the HTTP request pipeline.
app.UseCors(builder=>builder.
AllowAnyHeader()
.AllowAnyMethod()
.WithOrigins("http://localhost:4200"));




// 9999999
app.UseHttpsRedirection();

app.UseAuthorization();

// 00000000

app.MapControllers();

app.Run();
