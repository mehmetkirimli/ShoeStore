using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Repositories;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services;
using ShoeStore.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DbContext Ba�lant�s�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoesDb")));

//Repository ve Service Ba�lant�s�
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // IRepository interface'ini implemente eden Repository s�n�f�n� kullanacak.
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderManagerService, OrderManagerService>();

//AutoMapper Ba�lant�s�
builder.Services.AddAutoMapper(typeof(Program)); // AutoMapper'�n hangi s�n�flar� e�le�tirece�ini belirtiyoruz.


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
