using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using EmailServiceTools;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UMS.Application.Entities.Course.Commands.InsertCourse;
using UMS.Application.Entities.Course.Queries.GetCourses;
using UMS.Infrastructure.Abstraction.EmailSenderInterface;
using UMS.Infrastructure.EmailService;
using UMS.WebAPI;
using UMS.WebAPI.DTO;
using UMS.WebAPI.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
//builder.Services.AddScoped(typeof(umsContext),typeof(umsContext));
builder.Services.AddMediatR(typeof(InsertCourseCommand).GetTypeInfo().Assembly);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter()));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// KeyCLoak Configuration
builder.Services.AddMvc();

//Email Configuration
var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = builder.Configuration["https://localhost:8080/auth/realms/UMSRealm/UMS-WebApp"];
    o.Audience = builder.Configuration["UMS-WebApp"];
    o.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = c =>
        {
            c.NoResult();

            c.Response.StatusCode = 500;
            c.Response.ContentType = "text/plain";
            if (builder.Environment.IsDevelopment())
            {
                return c.Response.WriteAsync(c.Exception.ToString());
            }
            return c.Response.WriteAsync("An error occured processing your authentication.");
        }
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();