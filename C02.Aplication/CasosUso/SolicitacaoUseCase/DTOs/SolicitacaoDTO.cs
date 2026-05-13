using System;

namespace Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;

public class SolicitacaoDTO
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public string NomeCliente { get; set; } = String.Empty;
    public int IdImovel { get; set; }
    public string DescricaoImovel { get; set; } = String.Empty;
    public int IdEstadoSolicitacao { get; set; }
    public string EstadoSolicitacao { get; set; } = String.Empty;
    public DateTime Data { get; set; }
}

