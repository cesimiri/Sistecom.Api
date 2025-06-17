using Identity.Api.Interfaces;
using Identity.Api.Model;
using Identity.Api.Persistence.DataBase;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Modelo.Sistecom.Modelo.Database;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Define la variable para CORS
// Define la variable para CORS
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CORS CORREGIDO
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        if (builder.Environment.IsDevelopment())
                        {
                            // Desarrollo: permitir orígenes específicos de localhost
                            policy.WithOrigins(
                                    "https://localhost:7180",      // Tu Blazor HTTPS
                                    "http://localhost:7180",       // Tu Blazor HTTP
                                    "https://localhost:7171",      // Puerto alternativo HTTPS
                                    "http://localhost:5047",       // Blazor Server
                                    "http://localhost:5000",       // Puerto alternativo
                                    "http://localhost:3000",      // Otro puerto común
                                    "http://localhost:5213",        // Otro puerto común
                                     "https://localhost:44377"        // Otro puerto común
                                  )
                                  .AllowAnyHeader()
                                  .WithExposedHeaders("totalAmountPages")
                                  .AllowAnyMethod()
                                  .AllowCredentials();
                        }
                        else
                        {
                            // Producción: dominios específicos
                            policy.WithOrigins(
                                    "https://lconcordia.compugtech.com",    // Tu dominio principal
                                    "http://lconcordia.compugtech.com",     // HTTP fallback
                                    "https://www.lconcordia.compugtech.com", // Con www
                                    "http://www.lconcordia.compugtech.com",  // Con www HTTP
                                    "https://localhost:7180",      // Tu Blazor HTTPS
                                    "http://localhost:7180",       // Tu Blazor HTTP
                                    "https://localhost:7171",      // Puerto alternativo HTTPS
                                    "http://localhost:5047",       // Blazor Server
                                    "http://localhost:5000",       // Puerto alternativo
                                    "http://localhost:3000",      // Otro puerto común
                                    "http://localhost:5213"       // Otro puerto común

                                  )
                                  .AllowAnyHeader()
                                  .WithExposedHeaders("totalAmountPages")
                                  .AllowAnyMethod()
                                  .AllowCredentials();
                        }
                    });
});



// Add services to the container (equivalente a ConfigureServices)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .WithExposedHeaders("totalAmountPages")
                        .AllowAnyMethod();
                    });
});

// DbContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//  options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<ApplicationDBContext>(options =>
//{
//    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
        ClockSkew = TimeSpan.Zero
    });


//--------------------------------------
//SERVICIOS PERSONALIZADOS 
//--------------------------------------
// Servicios de Empresas 
builder.Services.AddScoped<IEmpresaCliente, EmpresaClienteService>();

// Servicios de Usuarios
builder.Services.AddScoped<IUsuario, UsuarioService>();

////// Servicios de Suscripciones
builder.Services.AddScoped<ISuscripcione, SuscripcioneService>();

////// Servicios de Proveedores
builder.Services.AddScoped<IProveedor, ProveedorService>();


// Servicios de Solicitudes de Compra
builder.Services.AddScoped<ISolicitudesCompra, SolicitudesCompraService>();

//FacturCompra
builder.Services.AddScoped<IFacturasCompra, FacturasCompraServices>();

// Servicios de Bodegas
builder.Services.AddScoped<IBodega,  BodegaService>();

// Servicios de Stock Bodega
builder.Services.AddScoped<IStockBodega, StockBodegaServices>();

//DetalleSolicitud
builder.Services.AddScoped<IDetalleSolicitud, DetalleSolicitudService>();

//Servicio OrdenesEtrega
builder.Services.AddScoped<IOrdenesEntrega, OrdenesEntregaServices>();

//Servicio DetalleFacturaCompra
builder.Services.AddScoped<IDetalleFacturaCompra, DetalleFacturaCompraService>();

//CategoriasProducto
builder.Services.AddScoped<ICategoriasProducto, CategoriasProductoSevices>();

//Producto
builder.Services.AddScoped<IProducto, ProductoServices>();

//Activo
builder.Services.AddScoped<IActivo, ActivoServices>();

//Mantenimiento
builder.Services.AddScoped<IMantenimiento, MantenimientoServices>();

//HistorialActivo
builder.Services.AddScoped<IHistorialActivo, HistorialActivoServices>();

//AsignacionesActivo
builder.Services.AddScoped<IAsignacionesActivo, AsignacionesActivoServices>();

//ServiciosServidor
builder.Services.AddScoped<IServiciosServidor, ServiciosServidorServices>();

//servidore
builder.Services.AddScoped<IServidore, ServidoreServices>();

//DetalleOrdenEntrega
builder.Services.AddScoped<IDetalleOrdenEntrega, DetalleOrdenEntregaServices>();

//AsignacionesLicencia
builder.Services.AddScoped<IAsignacionesLicencia, AsignacionesLicenciaServices>();

//TiposLicencium
builder.Services.AddScoped<ITiposLicencium, TiposLicenciumServices>();

//MovimientosInventario
builder.Services.AddScoped<IMovimientosInventario, MovimientosInventarioServices>();

//ComponentesEnsamblaje
builder.Services.AddScoped<IComponentesEnsamblaje, ComponentesEnsamblajeServices>();

//OrdenesEnsamblaje
builder.Services.AddScoped<IOrdenesEnsamblaje, OrdenesEnsamblajeServices>();

//DetalleEnsamblaje
builder.Services.AddScoped<IDetalleEnsamblaje, DetalleEnsamblajeServices>();

//Licencia
builder.Services.AddScoped<ILicencia, LicenciaServices>();

//Sucursale
builder.Services.AddScoped<ISucursale, SucursaleServices>();

//cargo
builder.Services.AddScoped<ICargo, CargoServices>();

//Departamento
builder.Services.AddScoped<IDepartamento, DepartamentoServices>();

//Marca
builder.Services.AddScoped<IMarca, MarcaServices>();





//Fin de servicios

//*builder.Services.AddScoped<IMenuInfo, MenuInfoServices>();
//builder.Services.AddScoped<IVollenda, VollendaServices>();
builder.Services.AddControllers();

// Construir la aplicación
var app = builder.Build();

// Configure the HTTP request pipeline (equivalente a Configure)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseCors(MyAllowSpecificOrigins); // CORS PRIMERO
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
