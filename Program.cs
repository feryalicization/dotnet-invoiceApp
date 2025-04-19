using InvoiceApp.Data; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Load koneksi string dari appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ✅ Konfigurasi DbContext dengan Pomelo.EntityFrameworkCore.MySql
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
);

// ✅ Tambahkan layanan MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ Konfigurasi pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ✅ Jangan lupa aktifkan Static Files

app.UseRouting();

app.UseAuthorization();

// ✅ Routing default controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
