using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.Command;

public class CadastrarProprietario(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb09_tipo_imovelModel> pesquisarTipo,
    Corretora.C01.Domain.Interfaces.ICadastrarRepositorio<tb18_proprietarioModel> cadastrar)
{
    public async Task<(CadastrarProprietarioDTO? dados, string mensagem, int codigo)> Executar(CadastrarProprietarioDTO dto)
    {
        var (tipo, _, _) = await pesquisarTipo.PesquisarPorIdAsync(dto.IdTipoImovel);
        if (tipo is null) return (null, "Tipo de imóvel não encontrado", 404);

        var model = new tb18_proprietarioModel
        {
            Nome = dto.Nome,
            Telefone = dto.Telefone,
            tb09_tipo_imovelModel = dto.IdTipoImovel
        };

        var (dado, mensagem, codigo) = await cadastrar.CadastrarAsync(model);
        return dado is null ? (null, mensagem, codigo) : (dto, mensagem, codigo);
    }
}

