using IMDbWebAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Detta gör att vi kommer kunna hämta data från web API:et med hjälp av JavaScript som kör inuti webbläsaren.
builder.Services.AddCors();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Detta gör att vi kommer kunna hämta data från web API:et med hjälp av JavaScript som kör inuti webbläsaren.
app.UseCors(builder => builder.AllowAnyOrigin());

app.MapControllers();

app.Run();
