using ADO.NET_Practical.Data.Contract;
using ADO.NET_Practical.Data.Implementation;
using ADO.NET_Practical.Services.Contract;
using ADO.NET_Practical.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS to allow requests from your frontend (localhost:5281)
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowClientApiApplication", builder =>
    {
        builder.WithOrigins("http://localhost:5281") // Specify the frontend origin
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// Apply the CORS policy globally here
app.UseCors("AllowClientApiApplication");

app.UseAuthorization();

app.MapControllers();

app.Run();
