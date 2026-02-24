using BackendApiWEB.Data;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.Data.Repositories;
using BackendApiWEB.Service;
using BackendApiWEB.Service.Implementations;
using BackendApiWEB.Service.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// 🔥 INJEÇÃO DE DEPENDÊNCIAS
// -----------------------------

// ✅ REGISTRA A CONEXÃO (Dapper)
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ✅ Melhor como Scoped
builder.Services.AddScoped<DbContextDapper>();

// REPOSITORIES
builder.Services.AddScoped<IUserRepository, UsuarioRepository>();
builder.Services.AddScoped<IPermissaoRepository, PermissaoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IResetSenhaRepository, ResetSenhaRepository>();

// SERVICES
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPermissaoService, PermissaoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// -----------------------------
// 🔥 CORS — permite Angular / Electron
// -----------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("DevCors");
app.MapControllers();

// ❌ NÃO FAÇA ISSO (fura o DI e pode quebrar)
/// var email = new EmailService();

app.Run();