using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Corretora.C02.Aplication.Servico.ITokenService;

namespace Corretora.C03.Infra.Servico.AuthService;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GerarToken(int usuarioId, string nome, string telefone, string role, string tipoUsuario)
    {
        string chave = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key não configurada.");
        string issuer = _configuration["Jwt:Issuer"] ?? "Corretora.API";
        string audience = _configuration["Jwt:Audience"] ?? "Corretora.Client";

        int expiraMinutos = 120;
        if (int.TryParse(_configuration["Jwt:ExpiryMinutes"], out int valor))
            expiraMinutos = valor;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, nome),
            new Claim("telefone", telefone),
            new Claim(ClaimTypes.Role, role),
            new Claim("tipo_usuario", tipoUsuario)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiraMinutos),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
