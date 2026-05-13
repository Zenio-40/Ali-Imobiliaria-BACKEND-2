using Corretora;
using Corretora.C03.Infra.Data;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Command;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.ClienteUseCase.Command;
using Corretora.C02.Aplication.CasosUso.ClienteUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.Command;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.FavoritoUseCase.Command;
using Corretora.C02.Aplication.CasosUso.FavoritoUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Command;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.PerfilUseCase.Command;
using Corretora.C02.Aplication.AuthUseCase.Command;
using Corretora.C03.Infra.Servico.AuthService;
using Corretora.C02.Aplication.Servico.IPasswordService;
using Corretora.C03.Infra.Servico.PasswordService;
using Corretora.C02.Aplication.Servico.ISmsService;
using Corretora.C03.Infra.Servico.ISmsService;
using Corretora.C02.Aplication.Servico.ITokenService;  // ✅ ITokenService

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Substitui AddOpenApi() por:
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insere: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        // ✅ Adicione esta linha
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<CorretoraDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CadastrarFuncionario>();
builder.Services.AddScoped<ActualizarFuncionario>();
builder.Services.AddScoped<DesativarFuncionario>();
builder.Services.AddScoped<RemoverFuncionario>();
builder.Services.AddScoped<PesquisarFuncionarioPorId>();
builder.Services.AddScoped<PesquisarTodosFuncionarios>();
builder.Services.AddScoped<LoginCommand>();
builder.Services.AddScoped<LoginUsuario>();

builder.Services.AddScoped<CadastrarCliente>();
builder.Services.AddScoped<ActualizarCliente>();
builder.Services.AddScoped<PesquisarClientePorId>();
builder.Services.AddScoped<PesquisarTodosClientes>();

builder.Services.AddScoped<CadastrarImovel>();
builder.Services.AddScoped<ActualizarImovel>();
builder.Services.AddScoped<DesativarImovel>();
builder.Services.AddScoped<PesquisarImovelPorId>();
builder.Services.AddScoped<PesquisarImoveisDisponiveis>();

builder.Services.AddScoped<AdicionarFavorito>();
builder.Services.AddScoped<RemoverFavorito>();
builder.Services.AddScoped<ListarFavoritosDoCliente>();

builder.Services.AddScoped<CadastrarSolicitacao>();
builder.Services.AddScoped<ActualizarEstadoSolicitacao>();
builder.Services.AddScoped<PesquisarSolicitacaoPorId>();
builder.Services.AddScoped<PesquisarSolicitacoesDoCliente>();


builder.Services.AddScoped<CadastrarPerfil>();

builder.Services.AddContratos();

// Services
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordVerify, PasswordVerifyService>();
builder.Services.AddScoped<IPasswordHash, PasswordHashService>();
builder.Services.AddScoped<IPasswordCreate, PasswordCreateService>(); // if exists
builder.Services.AddHttpClient<ISmsService, SmsService>();

// JWT Configuration (mantido para login mas middleware desativado para testes)
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "default_secret_key");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

