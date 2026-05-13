using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Command;

public class CadastrarSolicitacao(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb06_clienteModel> pesquisarCliente,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb11_imovelModel> pesquisarImovel,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb19_estado_solicitacaoModel> pesquisarEstado,
    Corretora.C01.Domain.Interfaces.ICadastrarRepositorio<tb20_solicitacaoModel> cadastrar)
{
    public async Task<(CadastrarSolicitacaoDTO? dados, string mensagem, int codigo)> Executar(CadastrarSolicitacaoDTO dto)
    {
        var (cliente, _, _) = await pesquisarCliente.PesquisarPorIdAsync(dto.IdCliente);
        if (cliente is null) return (null, "Cliente nao encontrado", 404);

        var (imovel, _, _) = await pesquisarImovel.PesquisarPorIdAsync(dto.IdImovel);
        if (imovel is null) return (null, "Imovel nao encontrado", 404);

        var idEstado = dto.IdEstadoSolicitacao <= 0 ? 1 : dto.IdEstadoSolicitacao;
        var (estado, _, _) = await pesquisarEstado.PesquisarPorIdAsync(idEstado);
        if (estado is null) return (null, "Estado de solicitacao nao encontrado", 404);

        var model = new tb20_solicitacaoModel
        {
            tb06_clienteModel = dto.IdCliente,
            tb11_imovelModel = dto.IdImovel,
            tb19_estado_solicitacaoModel = idEstado,
            Data = dto.DataPretendida ?? DateTime.UtcNow
        };

        var (dado, mensagem, codigo) = await cadastrar.CadastrarAsync(model);
        return dado is null ? (null, mensagem, codigo) : (dto, mensagem, codigo);
    }
}
