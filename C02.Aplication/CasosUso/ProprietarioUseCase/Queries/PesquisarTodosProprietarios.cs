using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.Queries;

public class PesquisarTodosProprietarios(Corretora.C01.Domain.Interfaces.IPesquisarTodosRepositorio<tb18_proprietarioModel> pesquisar)
{
    public async Task<(IEnumerable<ProprietarioDTO>? dados, string mensagem, int codigo)> Executar(int pagina = 1, int quantidade = 20)
    {
        var (proprietarios, mensagem, codigo) = await pesquisar.PesquisarTodosAsync(pagina, quantidade);
        if (proprietarios is null) return (null, mensagem, codigo);

        var dtoList = proprietarios.Select(p => new ProprietarioDTO
        {
            Id = p.Id,
            Nome = p.Nome,
            Telefone = p.Telefone,
            IdTipoImovel = p.tb09_tipo_imovelModel,
            TipoImovel = p.Imoveis.FirstOrDefault()?.TipoImovel?.Descricao ?? string.Empty
        });

        return (dtoList, "Proprietários encontrados com sucesso", 200);
    }
}

