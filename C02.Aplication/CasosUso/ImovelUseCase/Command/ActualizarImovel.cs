using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ImovelUseCase.Command;

public class ActualizarImovel(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb11_imovelModel> pesquisarImovel,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb09_tipo_imovelModel> pesquisarTipo,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb10_tipologiaModel> pesquisarTipologia,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb04_funcionarioModel> pesquisarFuncionario,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb18_proprietarioModel> pesquisarProprietario,
    Corretora.C01.Domain.Interfaces.IActualizarRepositorio<tb11_imovelModel> actualizar)
{
    public async Task<(ActualizarImovelDTO? dados, string mensagem, int codigo)> Executar(ActualizarImovelDTO dto)
    {
        var (imovel, _, _) = await pesquisarImovel.PesquisarPorIdAsync(dto.Id);
        if (imovel is null) return (null, "Imovel nao encontrado", 404);

        var (tipo, _, _) = await pesquisarTipo.PesquisarPorIdAsync(dto.IdTipoImovel);
        if (tipo is null) return (null, "Tipo de imovel nao encontrado", 404);

        var (tipologia, _, _) = await pesquisarTipologia.PesquisarPorIdAsync(dto.IdTipologia);
        if (tipologia is null) return (null, "Tipologia nao encontrada", 404);

        var (funcionario, _, _) = await pesquisarFuncionario.PesquisarPorIdAsync(dto.IdFuncionario);
        if (funcionario is null) return (null, "Funcionario nao encontrado", 404);

        if (dto.IdProprietario.HasValue)
        {
            var (proprietario, _, _) = await pesquisarProprietario.PesquisarPorIdAsync(dto.IdProprietario.Value);
            if (proprietario is null) return (null, "Proprietario nao encontrado", 404);
        }

        imovel.Descricao = dto.Descricao;
        imovel.Preco = dto.Preco;
        imovel.tb09_tipo_imovelModel = dto.IdTipoImovel;
        imovel.tb10_tipologiaModel = dto.IdTipologia;
        imovel.tb04_funcionarioModel = dto.IdFuncionario;
        imovel.tb18_proprietarioModel = dto.IdProprietario;
        imovel.Estado = dto.Estado;
        imovel.EstadoAprovacao = string.IsNullOrWhiteSpace(dto.EstadoAprovacao)
            ? imovel.EstadoAprovacao
            : dto.EstadoAprovacao;

        var (dado, mensagem, codigo) = await actualizar.ActualizarAsync(imovel);
        return (dto, mensagem, codigo);
    }
}
