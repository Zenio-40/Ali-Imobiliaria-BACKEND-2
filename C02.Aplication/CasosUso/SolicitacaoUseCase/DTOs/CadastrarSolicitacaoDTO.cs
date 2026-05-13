using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;

public class CadastrarSolicitacaoDTO
{
    [Required(ErrorMessage = "Informe o ID do cliente.")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "Informe o ID do imovel.")]
    public int IdImovel { get; set; }

    public int IdEstadoSolicitacao { get; set; } = 1;

    public DateTime? DataPretendida { get; set; }
}
