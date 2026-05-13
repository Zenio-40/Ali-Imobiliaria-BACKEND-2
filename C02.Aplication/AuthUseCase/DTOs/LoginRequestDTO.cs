using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.AuthUseCase.DTOs;

public class LoginRequestDTO
{
    [Required]
    [MaxLength(20)]
    public string Telefone { get; set; } = string.Empty;

    [Required]
    [MinLength(4)]
    [MaxLength(100)]
    public string Senha { get; set; } = string.Empty;
}