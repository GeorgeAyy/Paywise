using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

// Configure MongoDB settings
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection(nameof(MongoSettings)));

// Register MongoDB client as a singleton
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Register the MongoDB database instance
builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// Register ApplicationDbContext
builder.Services.AddSingleton<ApplicationDbContext>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new ApplicationDbContext(client, settings.DatabaseName);
});

// Register IPasswordHasher<User>
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IExpenseService, ExpenseService>();
builder.Services.AddSingleton<IReportService, ReportService>();
// Register the ServiceFactory
builder.Services.AddSingleton<IServiceFactory, ServiceFactory>();
// Add the simple logging library and its adapter
builder.Services.AddSingleton<SimpleLoggingLibrary>();
builder.Services.AddSingleton<IAppLogger, SimpleLoggingLibraryAdapter>(sp =>
{
    var simpleLoggingLibrary = sp.GetRequiredService<SimpleLoggingLibrary>();
    return new SimpleLoggingLibraryAdapter(simpleLoggingLibrary);
});

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Set the login path
        options.LogoutPath = "/Account/Logout";
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

// Use session middleware
app.UseSession();

// Use authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
