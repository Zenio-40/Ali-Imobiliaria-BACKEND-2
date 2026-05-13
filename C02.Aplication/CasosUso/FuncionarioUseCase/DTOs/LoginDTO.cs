using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "Informe o telefone.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    public string Senha { get; set; } = string.Empty;
}