using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.AuthUseCase.DTOs;

public class LoginResponseDTO
{
    public int UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
    public string TipoUsuario { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
