
using Eazy.Credit.API.Configurations.Swagger;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Identity.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Eazy.Credit.Security.Contracts.Identity;
using Eazy.Credit.Security.Identity.Services;
using Eazy.Credit.Security.Contracts.Auth;
using Eazy.Credit.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Eazy.Credit.Security.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Persistence.Services;
using EazyCoreObjs.Interfaces;
using EazyCoreObjs.Repos;
using static Org.BouncyCastle.Math.EC.ECCurve;
using EazyCoreObjs.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"]))
    };
});

builder.Services.AddSwaggerDocumentation();

const string AllowOrigins = "CorsPolicy"; //builder.Configuration["Cors"];

// Cors configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowOrigins, builder =>
    {
        builder.AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials();
    });
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(AllowOrigins, builder =>
//    {
//        builder.AllowAnyHeader()
//                .AllowAnyMethod()
//                .SetIsOriginAllowed(origin => true) // allow any origin
//                .AllowCredentials();
//    });
//});


builder.Services.AddDbContextPool<SecurityContext>((serviceProvider, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorkFlowCon"),
        sqlServerOptionsAction: sqlOptions =>
        {
            //sqlOptions.EnableRetryOnFailure(
            //    maxRetryCount: 10,
            //    maxRetryDelay: TimeSpan.FromSeconds(30),
            //    errorNumbersToAdd: null
            //    );
            sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        });
    //options.UseInternalServiceProvider(serviceProvider);
});

builder.Services.AddDbContextPool<PersistenceContext>((serviceProvider, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorkFlowCon"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
                );
            sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        });
});

builder.Services.AddDbContextPool<EazyCoreContext>((serviceProvider, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EazybankCon"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
                );
            sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        });
});


// Configure the HTTP request pipeline.

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.Configure<EmailSettingsDto>(builder.Configuration.GetSection("emailSettings"));
builder.Services.Configure<SendGridOptionsDto>(builder.Configuration.GetSection("SendGridEmailOptions"));



IConfigurationSection _IConfigurationSection = builder.Configuration.GetSection("IdentityDefaultOptions");
builder.Services.Configure<DefaultIdentityOptions>(_IConfigurationSection);
var _DefaultIdentityOptions = _IConfigurationSection.Get<DefaultIdentityOptions>();

builder.Services.AddIdentity<AppUsers, AppRoles>(options =>
    {
        options.Password.RequireDigit = _DefaultIdentityOptions.PasswordRequireDigit;
        options.Password.RequiredLength = _DefaultIdentityOptions.PasswordRequiredLength;
        options.Password.RequireNonAlphanumeric = _DefaultIdentityOptions.PasswordRequireNonAlphanumeric;
        options.Password.RequireUppercase = _DefaultIdentityOptions.PasswordRequireUppercase;
        options.Password.RequireLowercase = _DefaultIdentityOptions.PasswordRequireLowercase;
        options.Password.RequiredUniqueChars = _DefaultIdentityOptions.PasswordRequiredUniqueChars;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(_DefaultIdentityOptions.LockoutDefaultLockoutTimeSpanInMinutes);
        options.Lockout.MaxFailedAccessAttempts = _DefaultIdentityOptions.LockoutMaxFailedAccessAttempts;
        options.Lockout.AllowedForNewUsers = _DefaultIdentityOptions.LockoutAllowedForNewUsers;

        options.User.RequireUniqueEmail = _DefaultIdentityOptions.UserRequireUniqueEmail;

        options.SignIn.RequireConfirmedEmail = _DefaultIdentityOptions.SignInRequireConfirmedEmail;
        options.SignIn.RequireConfirmedAccount = _DefaultIdentityOptions.RequireConfirmedAccount;

        options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultEmailProvider;
        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;

        //options.Tokens.ProviderMap.Add("CustomEmailConfirmation",
        //new TokenProviderDescriptor(
        //    typeof(CustomEmailConfirmationTokenProvider<AppUsers>)));
        //options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

    }).AddEntityFrameworkStores<SecurityContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<DataProtectorTokenProvider<AppUsers>>("passwordrest");

builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromMinutes(30));
//builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(5));


builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IEmailsService, EmailsService>();
builder.Services.AddScoped<IAuthServicecs, AuthServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<IPrmRoleLimitService, PrmRoleLimitService>();
builder.Services.AddScoped<IWorkflowsService, WorkflowsService>();
builder.Services.AddScoped<IWorkflowLevelService, WorkflowLevelService>();
builder.Services.AddScoped<IEazyCore, EazyBaseRepos>();
builder.Services.AddScoped<IEazyBaseFuncAndProc, EazyBaseFuncAndProc>();
builder.Services.AddScoped<ICreditServices, CreditServices>();
builder.Services.AddScoped<ICreditServiceFuncAndProc, CreditServiceFuncAndProc>();
builder.Services.AddScoped<ICreateTransHeaderStageService, CreateTransHeaderStageService>();




// Add services to the container.

var app = builder.Build();


//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//});

//app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}


if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerDocumentation();
}

app.UseCors(AllowOrigins);

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
