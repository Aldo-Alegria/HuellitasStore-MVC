using ProyectoDesarrollo.B.P.Services;
using ProyectoDesarrollo.B.P.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VentasC>(options =>
{
    var cadena = builder.Configuration.GetConnectionString("VentasDB");
    if (string.IsNullOrWhiteSpace(cadena))
        cadena = Environment.GetEnvironmentVariable("ConnectionStrings__VentasDB");
    if (string.IsNullOrWhiteSpace(cadena))
        throw new InvalidOperationException("Configura ConnectionStrings__VentasDB antes de iniciar la aplicación.");
    options.UseSqlServer(cadena);
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.Add(new ServiceDescriptor(typeof(IUsuario),
    new UsuarioRepository()));

/* ----------------------------------------Mayerly---------------------------------------- */
builder.Services.Add(new ServiceDescriptor(typeof(iTemporalVenta),
                     new TemporalVentaRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(iProducto),
                     new ProductoRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(iUsuarioRegistro),
                     new UsuarioRegistroRepository()));

builder.Services.Add(new ServiceDescriptor(typeof(IDVenta),
                     new DVentaRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(IVenta),
                     new VentaRepository()));
/* --------------------------------------------------------------------------------------- */
builder.Services.AddControllersWithViews();


/*  ------------------------------------------------------- */

// HABILITANDO EL TIEMPO DE LAS SESIONES
builder.Services.AddSession(options =>
{
    options.IOTimeout = TimeSpan.FromSeconds(3600);
});

/*  ------------------------------------------------------- */


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/*  ------------------------------------------------------- */
app.UseSession(); // habilitando las sesiones
/*  ------------------------------------------------------- */

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Catalogo}/{id?}");

app.Run();
