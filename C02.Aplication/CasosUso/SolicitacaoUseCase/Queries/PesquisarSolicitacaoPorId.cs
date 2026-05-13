using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Queries;

public class PesquisarSolicitacaoPorId(Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb20_solicitacaoModel> pesquisar)
{
    public async Task<(SolicitacaoDTO? dados, string mensagem, int codigo)> Executar(int id)
    {
        var (solicitacao, mensagem, codigo) = await pesquisar.PesquisarPorIdAsync(id);
        if (solicitacao is null) return (null, mensagem, codigo);

        var dto = new SolicitacaoDTO
        {
            Id = solicitacao.Id,
            IdCliente = solicitacao.tb06_clienteModel,
            NomeCliente = solicitacao.Cliente?.Nome ?? string.Empty,
            IdImovel = solicitacao.tb11_imovelModel,
            DescricaoImovel = solicitacao.Imovel?.Descricao ?? string.Empty,
            IdEstadoSolicitacao = solicitacao.tb19_estado_solicitacaoModel,
            EstadoSolicitacao = solicitacao.EstadoSolicitacao?.Nome ?? string.Empty,
            Data = solicitacao.Data
        };

        return (dto, "Solicitação encontrada com sucesso", 200);
    }
}

