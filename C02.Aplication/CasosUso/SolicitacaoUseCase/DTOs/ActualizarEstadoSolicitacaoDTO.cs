using System;
using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;

public class ActualizarEstadoSolicitacaoDTO
{
    [Required(ErrorMessage = "Informe o ID da solicitação.")]
    public int IdSolicitacao { get; set; }

    [Required(ErrorMessage = "Informe o novo estado da solicitação.")]
    public int IdNovoEstado { get; set; }
}

