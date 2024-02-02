using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BankApplication.Core.Interfaces;
using BankApplication.Core.Services;
using BankApplication.Core.Utilities;
using BankApplication.Data;
using BankApplication.Data.Interfaces;
using BankApplication.Data.Repos;
using BankApplication.Domain.Profiles;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5265");

builder.Services.AddControllers();
builder.Services.AddSingleton<IBankApplicationContext, BankApplicationContext>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountTypeService, AccountTypeService>();
builder.Services.AddScoped<IAccountTypeRepo, AccountTypeRepo>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepo, AdminRepo>();
builder.Services.AddScoped<IUtilityMethods, UtilityMethods>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IDispositionService, DispositionService>();
builder.Services.AddScoped<IDispositionRepo, DispositionRepo>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanRepo, LoanRepo>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();

// Authentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    var configuration = builder.Configuration.GetSection("Jwt");
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Issuer"],
        ValidAudience = configuration["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]))
    };
});

// Automapper
builder.Services.AddAutoMapper(
    typeof(Program).Assembly, // could remove program later
    typeof(AccountCreationProfile).Assembly,
    typeof(AccountProfile).Assembly,
    typeof(CreateLoanProfile).Assembly,
    typeof(CustomerProfile).Assembly, 
    typeof(CustomerRegistrationProfile).Assembly,
    typeof(TransactionProfile).Assembly);


var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
