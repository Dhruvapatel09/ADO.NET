using ClientApplicationCore.Implementation;
using ClientApplicationCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowClientApplicationCore", builder =>
    {
        builder.WithOrigins("http://localhost:5139").WithOrigins("http://localhost:5281")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
// Add services to the container.
builder.Services.AddControllersWithViews();
//Register dependencies
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowClientApplicationCore");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
