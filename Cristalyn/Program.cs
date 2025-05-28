using Cristalyn.Data;
using Cristalyn.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Înregistrare DbContext
builder.Services.AddDbContext<CristalynContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Înregistrare servicii
builder.Services.AddScoped<ReduceriService>();

var app = builder.Build();

app.UseStaticFiles();

// Inițializare DB (opțional, doar dacă ai un DbInitializer)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CristalynContext>();
    // DbInitializer.Initialize(context); // dacă ai inițializare produse
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

