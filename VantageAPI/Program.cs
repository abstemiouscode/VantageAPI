using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VantageAPI;
using VantageAPI.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("CustomerList"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedDatabase(app);

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

void SeedDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<CustomerContext>();

        // Read data from JSON file
        var jsonData = File.ReadAllText("data.json");
        var customers = JsonSerializer.Deserialize<List<Customer>>(jsonData);

        // Add data to the context and save changes
        context.Customers.AddRange(customers);
        context.SaveChanges();
    }
}

