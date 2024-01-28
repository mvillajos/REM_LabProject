using LabProject.Data;
using LabProject.Repositories;
using LabProject.Repositories.Interfaces;
using LabProject.Services;
using LabProject.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesContext") ?? throw new InvalidOperationException("Connection string 'SalesContext' not found.")));
builder.Services.AddDbContext<CustomersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomersContext") ?? throw new InvalidOperationException("Connection string 'CustomersContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//configure repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

//configure services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped <IDashboardService, DashBoardService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
