using TesteICI.Infra.CrossCutting.IoC;
using TesteICI.Infra.CrossCutting.Security.Extensions;
using TesteICI.Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.RegisterDependencyInjection(builder.Configuration);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<SecurityDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<SecurityDbContext>();


builder.Services.AddControllersWithViews();

builder.Services.AddAuthenticationMvcConfig(builder.Configuration);

var app = builder.Build();
app.UseGlobalizationConfig();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
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

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
