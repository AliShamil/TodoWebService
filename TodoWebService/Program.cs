using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using TodoWebService;
using TodoWebService.Data;
using TodoWebService.Models.DTOs.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDomainServices();

builder.Services.AddLoggingPath(builder.Configuration);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDbConnectionString"));
});

builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:5000").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddSwagger();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseCors("NgOrigins");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
