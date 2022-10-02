using System.Reflection;
using System.Text;
using EmployeeTasks.EmployeeDbContext;
using EmployeeTasks.Extensions;
using EmployeeTasks.Helpers;
using EmployeeTasks.Migrations;
using EmployeeTasks.Models;
using EmployeeTasks.Services.Auth;
using EmployeeTasks.Services.Employee;
using EmployeeTasks.Services.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          ;
                      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

  
// inject

builder.Services.AddDbContext<UserTasksContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddSingleton<DapperDBContext>();
builder.Services.AddSingleton<Database>();





            // Add Service For The JWT Authentication
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<UserTasksContext>().
               AddDefaultTokenProviders();

            var appSettingsSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter())
            );


            // configure DI for AuthSErvices services
            builder.Services.AddScoped<IAuthServices, AuthServices>();

            // configure DI for Employee services
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();


            // configure DI for Employee services
            builder.Services.AddScoped<ITasksServices, TasksServices>();




builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
        .AddFluentMigratorCore()
        .ConfigureRunner(c => c.AddSqlServer2012()
            .WithGlobalConnectionString(builder.Configuration.GetConnectionString("SqlConnection"))
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

// builder.Services.AddDbContext<TasksContext>(options =>
//         options.UseNpgsql(builder.Configuration.GetConnectionString("TasksContextConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

// app.MigrateDatabase();

app.Run();
