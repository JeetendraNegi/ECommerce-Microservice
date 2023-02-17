using MessageBrokerService.AsyncMessaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagementService.Models;
using UserManagementService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register the service
builder.Services.AddScoped<IDataAccessService<UserDetails>, UserDataAccessService>();
builder.Services.AddScoped<IDataAccessService<AddressDetails>, AddressDataAccessService>();
builder.Services.AddScoped<IDataAccessService<CardDetails>, CardDataAccessService>();

//register async Messaging service
builder.Services.AddScoped<IMessageSender<AddressDetails>, MessageSender<AddressDetails>>();
builder.Services.AddScoped<IMessageSender<CardDetails>, MessageSender<CardDetails>>();
builder.Services.AddScoped<IMessageReceiver, MessageReceiver> ();

// Adding Authentication
var issuer = builder.Configuration["JWT:ValidIssuer"];
var audience = builder.Configuration["JWT:ValidAudience"];
List<string> Validissuer = new List<string>(issuer.Split(','));
List<string> Validaudience = new List<string>(audience.Split(','));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
