using BackendApiWEB.Data;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.Data.Repositories;
using BackendApiWEB.Service;
using BackendApiWEB.Service.Implementations;
using BackendApiWEB.Service.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// 🔥 INJEÇÃO DE DEPENDÊNCIAS
// -----------------------------
builder.Services.AddSingleton<DbContextDapper>();

// REPOSITORIES
builder.Services.AddScoped<IUserRepository, UsuarioRepository>();
builder.Services.AddScoped<IPermissaoRepository, PermissaoRepository>();

builder.Services.AddScoped<IResetSenhaRepository, ResetSenhaRepository>();


// SERVICES
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPermissaoService, PermissaoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// -----------------------------
// 🔥 CORS — permite Angular / Electron
// -----------------------------
builder.Services.AddCors(options => {
    options.AddPolicy("DevCors", policy => {
        policy
            .WithOrigins("http://localhost:4200", "http://localhost")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// -----------------------------
// 🔥 SWAGGER EM DEV
// -----------------------------
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// -----------------------------
// ❗ IMPORTANTE: SEM HTTPS REDIRECTION
// -----------------------------
// app.UseHttpsRedirection();

// -----------------------------
// 🔥 CORS
// -----------------------------
app.UseCors("DevCors");

// Controllers
app.MapControllers();
var email = new EmailService();
email.Enviar("gildevson@gmail.com", "Teste", "<h1>Funcionou!</h1>");
app.Run();
