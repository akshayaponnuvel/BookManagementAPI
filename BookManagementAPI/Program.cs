using BookManagementAPI.Data; // Ensure this matches your project's namespace
using BookManagementAPI.Middlewares; // Add this line
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName); // Ensure unique schema IDs
});

builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Add authentication if needed
// app.UseAuthentication(); // Uncomment if authentication is used

app.UseAuthorization();

// Use FastEndpoints middleware
app.UseFastEndpoints();

app.Run();
