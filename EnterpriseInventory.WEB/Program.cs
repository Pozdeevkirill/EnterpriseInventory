using EnterpriseInventory.BAL.Interfaces;
using EnterpriseInventory.BAL.Services;
using EnterpriseInventory.DAL.Data;
using EnterpriseInventory.DAL.Interfaces;
using EnterpriseInventory.DAL.Repositoryes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Database
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(option => 
    option.AddPolicy(name: MyAllowSpecificOrigins,
       policy =>
       {
           policy.WithOrigins("http://127.0.0.1:5500/");
       })
);

//Scope
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICabinetRepository, CabinetRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICabinetService, CabinetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
