using AstroApp.Repositories.Interfaces;
using AstroApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ---------------- Services ----------------

// Add MVC with views
builder.Services.AddControllersWithViews();

// Add API controllers (needed for [ApiController])
builder.Services.AddControllers();

// Register repository
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Enable CORS for local development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// ---------------- Middleware ----------------

// Use static files
app.UseStaticFiles();

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Use routing
app.UseRouting();

// Use CORS
app.UseCors("AllowAll");

// Use authorization (if any)
app.UseAuthorization();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Map MVC controllers (for Views)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Manage}/{id?}");

// Map API controllers (for [ApiController])
app.MapControllers();

app.Run();
