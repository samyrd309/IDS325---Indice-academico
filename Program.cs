using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IDS325___Indice_academico.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IDS325___Indice_academicoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IDS325___Indice_academicoContext") ?? throw new InvalidOperationException("Connection string 'IDS325___Indice_academicoContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calificacions}/{action=Index}/{id?}");


app.Run();
