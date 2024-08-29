
using AutoMapper;
using CRUDExa.DataAccess.Data;
using CRUDExa.Middlewares;
using CRUDExa.Persistencia.Data;
using CRUDExa.Persistencia.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var enviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{enviromentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.

var scretKey = builder.Configuration.GetSection("settings").GetSection("secretKey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(scretKey);

builder.Services.AddControllers();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer();

var enviroment = builder.Configuration.GetSection("enviroment").Value.ToString();
var version = builder.Configuration.GetSection("version").Value.ToString();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = $"IntranetCsi.Api {enviroment}", Version = $"{version}" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Es necesario el campo Bearer JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });


});

builder.Services.AddDbContext<ApplicationDbContext>(
           options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
       );

builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();

builder.Services.AddLogging(
     builder =>
     {
         builder.AddFilter("Microsoft", LogLevel.None)
                .AddFilter("System", LogLevel.None)
                .AddFilter("NToastNotify", LogLevel.None);
     });

builder.Services.AddApplicationInsightsTelemetry();
//builder.Services.AddAutoMapper(mc =>
//{
//    mc.ShouldMapMethod = (m => false);
//    mc.AddProfile(new AutoMapperConfig());
//}
//);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin()
    );
});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                 .AddJwtBearer(options => {
//                     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//                     {
//                         ValidateIssuer = true,
//                         ValidateAudience = true,
//                         ValidateLifetime = true,
//                         ValidateIssuerSigningKey = true,
//                         ValidIssuer = builder.Configuration["issuerLogin"],
//                         ValidAudience = builder.Configuration["audienceLogin"],
//                         IssuerSigningKey = new SymmetricSecurityKey(
//                             Encoding.UTF8.GetBytes(builder.Configuration["SecretKeyLogin"])
//                             ),
//                         ClockSkew = TimeSpan.Zero
//                     };
//                 });
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 52428800; // 50 MB
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiIntranetCsi v1");
    c.DocExpansion(DocExpansion.None);
}
);

app.UseCors(c =>
{
    c.WithOrigins("https://*.buzzword.com.mx", "https://127.0.0.1:8080").AllowAnyMethod();
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
