using Api.Domain.Entities;
using Api.Domain.Interfaces;
using DataAccess.EFCore;
using DataAccess.EFCore.Repositories;
using DataAccess.EFCore.UnitOfWork;
using First_api.common;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Use 'builder.Services' instead of just 'services'
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        // This tells EF to look for migrations in your Class Library assembly
        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
    ));


//builder.Services.AddIdentity<User, Role>()
//    .AddEntityFrameworkStores<ApplicationContext>()
//    .AddDefaultTokenProviders();

// Added 'cfg => { }' as the first argument
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

// Add Identity Services
//builder.Services.AddIdentity<User, Role>() // Use your custom Role class here
//    .AddEntityFrameworkStores<ApplicationContext>()
//    .AddDefaultTokenProviders();
builder.Services.AddIdentityServices(builder.Configuration);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Registering the Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Registering specific Repositories
builder.Services.AddScoped<IDeveloperRepository, DeveloperRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
// Configures Swagger to support JWT Bearer authorization and displays the Lock Icon

builder.Services.AddSwaggerGen(options =>
{
    // Define the scheme as usual
 

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // 👈 CHANGED: Uses native HTTP scheme
        Scheme = "bearer",               // 👈 CHANGED: Must be lowercase "bearer"
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste your JWT token below (Do NOT type 'Bearer ' manually, Swagger will add it)"
    });

    // 🔄 REPLACE the old AddSecurityRequirement with this filter:
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
