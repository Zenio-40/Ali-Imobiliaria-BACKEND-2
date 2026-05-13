using Corretora.C01.Domain;

namespace Corretora.C02.Aplication.CasosUso.FavoritoUseCase.Command;

public class RemoverFavorito(Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb21_favoritoModel> remover)
{
    public async Task<(bool sucesso, string mensagem, int codigo)> Executar(int id)
    {
        return await remover.RemoverAsync(id);
    }
}

