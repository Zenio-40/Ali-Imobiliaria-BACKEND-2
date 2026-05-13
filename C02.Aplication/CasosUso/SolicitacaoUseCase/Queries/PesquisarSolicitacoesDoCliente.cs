using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Queries;

public class PesquisarSolicitacoesDoCliente(Corretora.C01.Domain.Interfaces.IPesquisarTodosRepositorio<tb20_solicitacaoModel> pesquisar)
{
    public async Task<(IEnumerable<SolicitacaoDTO>? dados, string mensagem, int codigo)> Executar(int idCliente, int pagina = 1, int quantidade = 20)
    {
        var (solicitacoes, mensagem, codigo) = await pesquisar.PesquisarTodosAsync(pagina, quantidade);
        if (solicitacoes is null) return (null, mensagem, codigo);

        var doCliente = solicitacoes
            .Where(s => s.tb06_clienteModel == idCliente)
            .Select(s => new SolicitacaoDTO
            {
                Id = s.Id,
                IdCliente = s.tb06_clienteModel,
                NomeCliente = s.Cliente?.Nome ?? string.Empty,
                IdImovel = s.tb11_imovelModel,
                DescricaoImovel = s.Imovel?.Descricao ?? string.Empty,
                IdEstadoSolicitacao = s.tb19_estado_solicitacaoModel,
                EstadoSolicitacao = s.EstadoSolicitacao?.Nome ?? string.Empty,
                Data = s.Data
            });

        return (doCliente, "Solicitações encontradas", 200);
    }
}

