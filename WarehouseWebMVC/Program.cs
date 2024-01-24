using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.MappingProfiles;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.Services.Impl;
using WarehouseWebMVC.Services.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(
		options => options.UseSqlite(builder.Configuration.GetConnectionString("WarehouseDB"))
	);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<SendMailService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWarehouseService, WarehouseSerivce>();
builder.Services.AddScoped<IReceiptService, ReceiptSerivce>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
