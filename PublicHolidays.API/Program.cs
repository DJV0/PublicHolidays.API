using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PublicHolidays.API.Extensions;
using PublicHolidays.Domain;
using PublicHolidays.Domain.Extensions;
using PublicHolidays.Domain.Mappers;
using PublicHolidays.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PublicHolidaysDb")));

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Public Holidays API";
    config.Version = "v1";
    config.Description = "API to get countries public holidays";
});

builder.Services.AddHttpClients();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
