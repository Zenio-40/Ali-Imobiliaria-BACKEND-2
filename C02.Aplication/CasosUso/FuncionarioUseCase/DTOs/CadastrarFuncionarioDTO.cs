using System;
using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;

public class CadastrarFuncionarioDTO
{
    [Required(ErrorMessage = "Informe o nome do funcionario.")]
    public string FuncionarioNome { get; set; } = String.Empty;


    [Required(ErrorMessage = "Informe o numero de telefone do funcionario.")]
    [MaxLength(9)]
    public string FuncionarioTelefone { get; set; } = String.Empty;
    
    
    [Required(ErrorMessage = "Selecione o perfil do funcionario.")]
    public int FuncionarioIdPerfil { get; set; }

}
    