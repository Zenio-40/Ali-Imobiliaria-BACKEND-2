using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.FavoritoUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.FavoritoUseCase.Command;

public class AdicionarFavorito(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb06_clienteModel> pesquisarCliente,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb11_imovelModel> pesquisarImovel,
    Corretora.C01.Domain.Interfaces.ICadastrarRepositorio<tb21_favoritoModel> cadastrar)
{
    public async Task<(AdicionarFavoritoDTO? dados, string mensagem, int codigo)> Executar(AdicionarFavoritoDTO dto)
    {
        var (cliente, _, _) = await pesquisarCliente.PesquisarPorIdAsync(dto.IdCliente);
        if (cliente is null) return (null, "Cliente não encontrado", 404);

        var (imovel, _, _) = await pesquisarImovel.PesquisarPorIdAsync(dto.IdImovel);
        if (imovel is null) return (null, "Imóvel não encontrado", 404);

        var model = new tb21_favoritoModel
        {
            tb06_clienteModel = dto.IdCliente,
            tb11_imovelModel = dto.IdImovel
        };

        var (dado, mensagem, codigo) = await cadastrar.CadastrarAsync(model);
        return dado is null ? (null, mensagem, codigo) : (dto, mensagem, codigo);
    }
}

