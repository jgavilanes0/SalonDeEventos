using Microsoft.EntityFrameworkCore;
using SalonEventos.Components;
using SalonEventos.Data;
using SalonEventos.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<SalonEventosDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepositorioClientes, RepositorioClientes>();
builder.Services.AddScoped<IRepositorioSedes, RepositorioSedes>();
builder.Services.AddScoped<IRepositorioServicios, RepositorioServicios>();
builder.Services.AddScoped<IRepositorioMobiliarios, RepositorioMobiliarios>();
builder.Services.AddScoped<IRepositorioEventos, RepositorioEventos>();
builder.Services.AddScoped<IRepositorioPagos, RepositorioPagos>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
