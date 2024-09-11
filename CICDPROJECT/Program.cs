using ApiToolkit.Net;
using CICDPROJECT.Model;
using CICDPROJECT.Model.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var config = new Config
//{
//    ApiKey = "",
//    Debug = false,      
//    Tags = new List<string> { "environment: production", "region: us-east-1" },

//};
//var client = await APIToolkit.NewClientAsync(config);


builder.Services.AddControllers();
builder.Services.AddTransient<LocationConfiguration>();
builder.Services.Configure<OpenWeatherOption>(builder.Configuration.GetSection("OpenWeather"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.Use(async (context, next) =>
//{
//    var apiToolkit = new APIToolkit(next, client);
//    await apiToolkit.InvokeAsync(context);
//});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
