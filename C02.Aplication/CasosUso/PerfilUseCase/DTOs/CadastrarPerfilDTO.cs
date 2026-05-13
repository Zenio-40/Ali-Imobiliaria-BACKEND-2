using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.PerfilUseCase.DTOs;

public class CadastrarPerfilDTO
{
    [Required(ErrorMessage = "Informe a descrição do perfil.")]
    public string Descricao { get; set; } = string.Empty;
}

