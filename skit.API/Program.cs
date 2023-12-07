using FluentValidation.AspNetCore;
using skit.API.Extensions;
using skit.API.Filters;
using skit.Application;
using skit.Core;
using skit.Infrastructure;
using skit.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ExceptionFilter());
});

builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddApplication();
builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

Globals.ApplicationUrl = builder.Configuration.GetValue<string>("ApplicationConfig:ApplicationUrl");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
