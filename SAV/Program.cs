using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAV.Data;
using SAV.Models;
using SAV.Services.Impl;
using SAV.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add the DbContext to use SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add controllers with views (for MVC or API controllers)
builder.Services.AddControllersWithViews(); // or builder.Services.AddControllers(); for API only

// Add Swagger for API documentation (optional for APIs)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ILoginService, LoginServiceImpl>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IReclamationService, ReclamationService>();
builder.Services.AddScoped<IInterventionService, InterventionService>();



// Add authentication and authorization services
builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Customize login path if needed
    });

// Add authorization policy for roles (SAV role, CLIENT role)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SAVRolePolicy", policy =>
        policy.RequireRole("SAV"));
    options.AddPolicy("ClientRolePolicy", policy =>
        policy.RequireRole("CLIENT"));
});

var app = builder.Build();

// Initialize roles during startup (this seeding process is done once)
await SeedRolesAsync(app.Services);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add authentication and authorization middleware
app.UseAuthentication();  // Needed for authentication
app.UseAuthorization();   // Needed for authorization

app.MapControllers();

app.Run();

// Role seeding method
// Role seeding method
static async Task SeedRolesAsync(IServiceProvider services)
{
    // Create a scope to resolve scoped services
    using (var scope = services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roleNames = new[] { "SAV", "CLIENT" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
