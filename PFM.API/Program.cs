using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PFM.Application.IServices;
using PFM.Application.Services;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using PFM.Infrastructure.Repository;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(Options => Options.UseSqlServer
(builder.Configuration.GetConnectionString("DBConnection")
));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"];
        var jwtIssuer = builder.Configuration["Jwt:Issuer"];
        var jwtAudience = builder.Configuration["Jwt:Audience"];

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)),

                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,

                ValidateAudience = true,
                ValidAudience = jwtAudience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
    });
//Account
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//user 
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//PassWord
builder.Services.AddScoped<IPasswordServices, PasswordServices>();
//JWT
builder.Services.AddScoped<IJWTServices, JWTServices>();
//Budget
builder.Services.AddScoped<IBudgetServices, BudgetServices>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
//Debts
builder.Services.AddScoped<IDebtServices, DebtServices>();
builder.Services.AddScoped<IDebtRepository, DebtRepository>();
//Roles
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
//SavingGole
builder.Services.AddScoped<ISavingGoleServices, SavingGoleServices>();
builder.Services.AddScoped<ISavingGoleRepository, SavingGoleRepository>();
//Transaction
builder.Services.AddScoped<ITransactionServices, TransactionServices>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
//Categorie
builder.Services.AddScoped<ICategorieServices, CategorieServices>();
builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();
//RolePermission
builder.Services.AddScoped<IRolePermissionServices, RolePermissionServices>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
//Investment
builder.Services.AddScoped<IInvestmentServices, InvestmentServices>();
builder.Services.AddScoped<IInvestmentRepository, InvestmentRepository>();
//UserRole
builder.Services.AddScoped<IUserRoleServices, UserRoleServices>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
//Permission
builder.Services.AddScoped<IPermissionServices, PermissionServices>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
//DashBord
builder.Services.AddScoped<IDashboardServices, DashboardServices>();


var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
