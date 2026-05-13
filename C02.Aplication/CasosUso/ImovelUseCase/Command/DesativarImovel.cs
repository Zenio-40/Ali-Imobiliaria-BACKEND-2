using Corretora.C01.Domain;

namespace Corretora.C02.Aplication.CasosUso.ImovelUseCase.Command;

public class DesativarImovel(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb11_imovelModel> pesquisar,
    Corretora.C01.Domain.Interfaces.IActualizarRepositorio<tb11_imovelModel> actualizar)
{
    public async Task<(bool sucesso, string mensagem, int codigo)> Executar(int id)
    {
        var (imovel, _, _) = await pesquisar.PesquisarPorIdAsync(id);
        if (imovel is null) return (false, "Imóvel não encontrado", 404);

        imovel.Estado = false;
        var (_, mensagem, codigo) = await actualizar.ActualizarAsync(imovel);
        return (codigo == 200, mensagem, codigo);
    }
}

