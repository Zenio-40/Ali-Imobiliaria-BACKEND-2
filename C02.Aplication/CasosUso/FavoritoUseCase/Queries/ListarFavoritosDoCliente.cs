using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.FavoritoUseCase.Queries;

public class ListarFavoritosDoCliente(Corretora.C01.Domain.Interfaces.IPesquisarTodosRepositorio<tb21_favoritoModel> pesquisar)
{
    public async Task<(IEnumerable<ImovelDTO>? dados, string mensagem, int codigo)> Executar(int idCliente, int pagina = 1, int quantidade = 20)
    {
        var (favoritos, mensagem, codigo) = await pesquisar.PesquisarTodosAsync(pagina, quantidade);
        if (favoritos is null) return (null, mensagem, codigo);

        var doCliente = favoritos
            .Where(f => f.tb06_clienteModel == idCliente)
            .Select(f => new ImovelDTO
            {
                Id = f.Imovel.Id,
                Descricao = f.Imovel.Descricao,
                Preco = f.Imovel.Preco,
                Estado = f.Imovel.Estado,
                IdTipoImovel = f.Imovel.tb09_tipo_imovelModel,
                TipoImovel = f.Imovel.TipoImovel?.Descricao ?? string.Empty,
                IdTipologia = f.Imovel.tb10_tipologiaModel,
                Tipologia = f.Imovel.Tipologia?.Descricao ?? string.Empty,
                IdFuncionario = f.Imovel.tb04_funcionarioModel,
                Funcionario = f.Imovel.Funcionario?.Nome ?? string.Empty,
                IdProprietario = f.Imovel.tb18_proprietarioModel,
                Proprietario = f.Imovel.ProprietarioModel?.Nome ?? string.Empty
            });

        return (doCliente, "Favoritos encontrados", 200);
    }
}

