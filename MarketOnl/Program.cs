using AspNetCoreHero.ToastNotification;
using MarketOnl.Data;
using MarketOnl.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 5; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddDbContext<BanHangOnlContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("BanHangOnlContext")));



builder.Services.AddSession(cfg =>
{
    cfg.Cookie.IsEssential = true;
    cfg.IdleTimeout = new TimeSpan(0, 15, 0);

});


builder.Services.AddMvc(cfg =>
{
    cfg.Filters.Add(new CustomActionFilter());
});




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
app.UseSession();   
app.UseAuthorization();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
