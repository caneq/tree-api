using TreesApi.BusinessLogic.DependencyInjection;
using TreesApi.Web.DependencyInjection;
using TreesApi.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddBusinessLogic(connection);
builder.Services.AddTreesApiServices();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
