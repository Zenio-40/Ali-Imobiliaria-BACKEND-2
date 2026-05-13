using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.Command;

public class ActualizarProprietario(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb18_proprietarioModel> pesquisar,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb09_tipo_imovelModel> pesquisarTipo,
    Corretora.C01.Domain.Interfaces.IActualizarRepositorio<tb18_proprietarioModel> actualizar)
{
    public async Task<(ActualizarProprietarioDTO? dados, string mensagem, int codigo)> Executar(ActualizarProprietarioDTO dto)
    {
        var (proprietario, _, _) = await pesquisar.PesquisarPorIdAsync(dto.Id);
        if (proprietario is null) return (null, "Proprietário não encontrado", 404);

        var (tipo, _, _) = await pesquisarTipo.PesquisarPorIdAsync(dto.IdTipoImovel);
        if (tipo is null) return (null, "Tipo de imóvel não encontrado", 404);

        proprietario.Nome = dto.Nome;
        proprietario.Telefone = dto.Telefone;
        proprietario.tb09_tipo_imovelModel = dto.IdTipoImovel;

        var (_, mensagem, codigo) = await actualizar.ActualizarAsync(proprietario);
        return (dto, mensagem, codigo);
    }
}

