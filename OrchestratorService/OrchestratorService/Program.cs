using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register the Ocelot
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
     .AddJsonFile("ocelot-dev.json", optional: false, reloadOnChange: true)
     .AddEnvironmentVariables();

builder.Services.AddOcelot();

// add cors

builder.Services.AddCors(p => p.AddPolicy("MyPolicy", b => {
    b.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.UseOcelot().Wait();

app.Run();
