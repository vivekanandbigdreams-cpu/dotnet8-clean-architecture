using Api.Domain.Entities;
using Api.Domain.Interfaces;
using DataAccess.EFCore;
using DataAccess.EFCore.Repositories;
using DataAccess.EFCore.UnitOfWork;
using Finance.API.common;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        // This tells EF to look for migrations in your Class Library assembly
        //b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
    ));

//builder.Services.AddIdentity<User, Role>()
//    .AddEntityFrameworkStores<ApplicationContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(...);


// Added 'cfg => { }' as the first argument
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());


// Call your new centralized method
builder.Services.AddIdentityServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the UnitOfWork with a Scoped lifetime
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IFinanceRepository, FinanceRepository>();
// Registering the Generic Repository
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));



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
app.UseAuthentication(); // Must be before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
