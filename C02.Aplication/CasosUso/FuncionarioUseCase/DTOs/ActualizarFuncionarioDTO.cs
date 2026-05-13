using System;
using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;

public class ActualizarFuncionarioDTO
{
    [Required(ErrorMessage = "Informe o ID do funcionário.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do funcionário.")]
    public string FuncionarioNome { get; set; } = String.Empty;

    [Required(ErrorMessage = "Informe o número de telefone do funcionário.")]
    [MaxLength(9)]
    public string FuncionarioTelefone { get; set; } = String.Empty;

    [Required(ErrorMessage = "Selecione o perfil do funcionário.")]
    public int FuncionarioIdPerfil { get; set; }

    public bool Estado { get; set; } = true;
}