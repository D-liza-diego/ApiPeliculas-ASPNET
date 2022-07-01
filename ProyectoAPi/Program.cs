using Microsoft.EntityFrameworkCore;
using ProyectoAPi;
using ProyectoAPi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<ApiContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<IAlmacenarArchivos, AlmacenadorLocal>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
