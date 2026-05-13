using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.Queries;

public class PesquisarProprietarioPorId(Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb18_proprietarioModel> pesquisar)
{
    public async Task<(ProprietarioDTO? dados, string mensagem, int codigo)> Executar(int id)
    {
        var (proprietario, mensagem, codigo) = await pesquisar.PesquisarPorIdAsync(id);
        if (proprietario is null) return (null, mensagem, codigo);

        var dto = new ProprietarioDTO
        {
            Id = proprietario.Id,
            Nome = proprietario.Nome,
            Telefone = proprietario.Telefone,
            IdTipoImovel = proprietario.tb09_tipo_imovelModel,
            TipoImovel = proprietario.Imoveis.FirstOrDefault()?.TipoImovel?.Descricao ?? string.Empty
        };

        return (dto, "Proprietário encontrado com sucesso", 200);
    }
}

