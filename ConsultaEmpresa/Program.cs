using Microsoft.EntityFrameworkCore;
using ConsultaEmpresa.Infra.Context;
using ConsultaEmpresa.Aplication.Services;
using ConsultaEmpresa.Domain.Features.Usuarios;
using ConsultaEmpresa.Infra.Repository.Usuarios;
using ConsultaEmpresa.Domain.Features.Shared;
using ConsultaEmpresa.Infra.Auth;
using Microsoft.OpenApi.Models;
using ConsultaEmpresa.Infra;
using System.Diagnostics;
using ConsultaEmpresa.Domain.Features.Empresas;
using ConsultaEmpresa.Infra.Repository.Empresas; // Add this namespace for opening the browser

var builder = WebApplication.CreateBuilder(args);

// Ensure the connection string is correctly configured
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register AppDbContext with the dependency injection container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Número de tentativas de repetição
            maxRetryDelay: TimeSpan.FromSeconds(30), // Atraso máximo entre tentativas
            errorNumbersToAdd: null // Opcional: especificar números de erro SQL para repetir
        )
    )
);

// Register repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();

// Register services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<GeradorDeTokenService>();

// Register the SenhaEncriptador service
builder.Services.AddScoped<SenhaEncriptador>();

// Add this line to register controllers
builder.Services.AddControllers();

// Register HttpClient service
builder.Services.AddHttpClient();

// JWT configuration
var minutosDeDuracao = builder.Configuration.GetValue<uint>("Settings:JWT:minutosDeDuracao");
var chaveDeAssinatura = builder.Configuration.GetValue<string>("Settings:JWT:chaveDeAssinatura");

builder.Services.AddScoped<IGeradorDeTokenDeAcesso>(
            options => new GeradorDeTokenDeAcesso(minutosDeDuracao, chaveDeAssinatura));
builder.Services.AddScoped<GeradorDeTokenService>();
builder.Services.AddScoped<IValidadorToken>(opt => new ValidadorToken(chaveDeAssinatura));

// Add Authorization services
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Insira 'Bearer' [espa�o] e o token JWT. Exemplo: 'Bearer eyJhbGciOiJI...'"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Open Swagger in the default browser
if (app.Environment.IsDevelopment())
{
    var swaggerUrl = "http://localhost:5059/swagger/index.html";
    Process.Start(new ProcessStartInfo
    {
        FileName = swaggerUrl,
        UseShellExecute = true
    });
}