using Microsoft.EntityFrameworkCore;
using Teste_de_conhecimento_Campos_Dealer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuração do serviço de HttpClient para chamadas de API
builder.Services.AddScoped<IApiService, ApiService>();

builder.Services.AddDbContext<AppBdContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("CamposDealer")));
// Configuração do serviço de HttpClient para chamadas de API
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppBdContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//DataInitializer.Initialize(app.Services).Wait();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppBdContext>();
        DataInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
app.Run();
