using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Command;

public class ActualizarEstadoSolicitacao(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb20_solicitacaoModel> pesquisarSolicitacao,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb19_estado_solicitacaoModel> pesquisarEstado,
    Corretora.C01.Domain.Interfaces.IActualizarRepositorio<tb20_solicitacaoModel> actualizar)
{
    public async Task<(ActualizarEstadoSolicitacaoDTO? dados, string mensagem, int codigo)> Executar(ActualizarEstadoSolicitacaoDTO dto)
    {
        var (solicitacao, _, _) = await pesquisarSolicitacao.PesquisarPorIdAsync(dto.IdSolicitacao);
        if (solicitacao is null) return (null, "Solicitação não encontrada", 404);

        var (estado, _, _) = await pesquisarEstado.PesquisarPorIdAsync(dto.IdNovoEstado);
        if (estado is null) return (null, "Estado de solicitação não encontrado", 404);

        solicitacao.tb19_estado_solicitacaoModel = dto.IdNovoEstado;

        var (_, mensagem, codigo) = await actualizar.ActualizarAsync(solicitacao);
        return (dto, mensagem, codigo);
    }
}

