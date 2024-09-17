using social_app_backend.AppDataContext;
using social_app_backend.Services;
using social_app_backend.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings")); 
builder.Services.AddSingleton<AppDbContext>(); 
builder.Services.AddScoped<UserServices>();


var app = builder.Build();

//always use https
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

