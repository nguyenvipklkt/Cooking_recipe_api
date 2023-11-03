using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Quartz;
using CookingRecipeApi.Helpers.SocketHelper;
using CookingRecipeApi.Helpers.QuaztHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Schedule Management", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// CORS
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// get apition in appsetting
var apiOption = builder.Configuration.GetSection("ApiOptions").Get<ApiOption>();
builder.Services.AddSingleton(apiOption);

// Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = apiOption.ValidAudience,
            ValidIssuer = apiOption.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiOption.Secret))
        };
    });

// connect Db
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(apiOption.StringConnection)
);

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile(new MappingContext());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Quartz setup
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey("SendNotifyJob");
    q.AddJob<SendNotify>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SendNotifyJob-trigger")
        .WithCronSchedule("*/30 * * * * ?")
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// Set up Socket
builder.Services.AddTransient<ConnectionManager>();
builder.Services.AddSingleton<ChatHandler>();

//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//web socket
var webSocketOptions = new WebSocketOptions()
                       {
                           KeepAliveInterval = TimeSpan.FromSeconds(120),
                           ReceiveBufferSize = 4 * 1024
                       };
app.UseWebSockets(webSocketOptions);
var chatHandler = app.Services.GetService<ChatHandler>();
app.UseMiddleware<WebSocketMiddlewareCustomer>(chatHandler);

app.UseCors();

app.UseStaticFiles();

app.UseRouting();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cooking Recipe v1");
    //
    c.DocumentTitle = "Cooking Recipe API";
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
