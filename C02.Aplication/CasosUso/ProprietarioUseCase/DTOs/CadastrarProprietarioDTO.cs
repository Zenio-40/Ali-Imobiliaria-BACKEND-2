using System;
using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.DTOs;

public class CadastrarProprietarioDTO
{
    [Required(ErrorMessage = "Informe o nome do proprietário.")]
    public string Nome { get; set; } = String.Empty;

    [Required(ErrorMessage = "Informe o telefone do proprietário.")]
    [MaxLength(9)]
    public string Telefone { get; set; } = String.Empty;

    [Required(ErrorMessage = "Selecione o tipo de imóvel.")]
    public int IdTipoImovel { get; set; }
}

