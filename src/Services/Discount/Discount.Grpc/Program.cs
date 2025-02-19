

using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountContext>(options =>
{
	options.EnableSensitiveDataLogging();
	options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});	
var app = builder.Build();

// Configure the HTTP request pipeline.
//Create Database, Run Migrations and seed data
app.UseMigrations();
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
