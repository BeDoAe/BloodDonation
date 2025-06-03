using BloodDonation.Helper;
using BloodDonation.Models;
using BloodDonation.Repositories.AccountRepo;
using BloodDonation.Repositories.DonateBloodRepo;
using BloodDonation.Repositories.HealthStatusRepo;
using BloodDonation.Repositories.HospitalRepo;
using BloodDonation.Repositories.RequestBloodRepo;
using BloodDonation.Repositories.UserHealthStatusRepo;
using BloodDonation.Services.AccountServ;
using BloodDonation.Services.DonateBloodServ;
using BloodDonation.Services.HealthStatusServ;
using BloodDonation.Services.HospitalServ;
using BloodDonation.Services.RequestBloodServ;
using BloodDonation.Services.UserHealthStatusServ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add Database Context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// 🔹 Configure JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Change to true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero, // Removes default 5-minute leeway
        ValidateLifetime = true
    };
});

// 🔹 Swagger with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BloodDonation API", Version = "v1" });

    // Add JWT auth to Swagger
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter JWT Bearer token ONLY. Example: **Bearer {your_token}**",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});

// 🔹 Enable Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DonorOrRecipient", policy =>
        policy.RequireRole("Donor", "Recipient"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IHealthStatusRepository, HealthStatusRepository>();
builder.Services.AddScoped<IHealthStatusService, HealthStatusService>();

builder.Services.AddScoped<IUserHealthStatusRepoistory, UserHealthStatusRepoistory>();
builder.Services.AddScoped<IUserHealthStatusService, UserHealthStatusService>();

builder.Services.AddScoped<IRequestBloodRepository, RequestBloodRepository>();
builder.Services.AddScoped<IRequestBloodService, RequestBloodService>();


builder.Services.AddScoped<IDonateBloodRepository, DonateBloodRepository>();
builder.Services.AddScoped<IDonateBloodService, DonateBloodService>();


builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IHospitalService, HospitalService>();






////////////////////////////////////////////////////////////////////////////////////////////////////
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
