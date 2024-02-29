using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using OfficeOpenXml;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.MappingProfiles;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.Services.Impl;
using WarehouseWebMVC.Utils.Mail;
using WarehouseWebMVC.Utils.Helper;
using WarehouseWebMVC.Utils.Helper.Impl;

var builder = WebApplication.CreateBuilder(args);

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(
		options => options.UseSqlite(builder.Configuration.GetConnectionString("WarehouseDB"))
	);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<SendMailUtil>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImportNoteService, ImportNoteSerivce>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IWarehouseService, WarehouseSerivce>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IAddressHelper, AddressHelper>();
builder.Services.AddScoped<IResetPasswordHelper, ResetPasswordHelper>();

// Enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// JSON Serializer
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
    .Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithRedirects("/Error/Error/{0}");
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
