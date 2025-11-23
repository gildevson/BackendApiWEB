using BackendApiWEB.Data;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.Data.Repositories;
using BackendApiWEB.Service;
using BackendApiWEB.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// 🔥 INJEÇÃO DE DEPENDÊNCIA
// -----------------------------
builder.Services.AddSingleton<DbContextDapper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

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
// 🚫 IMPORTANTE: REMOVER HTTPS REDIRECTION
// -----------------------------
// Isto estava quebrando seu Angular/Electron
// app.UseHttpsRedirection();

// -----------------------------
// 🔥 CORS ATIVO
// -----------------------------
app.UseCors("DevCors");

// Controllers
app.MapControllers();

app.Run();
