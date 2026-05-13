using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E21_favorito;

public class RemoverFavoritoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb21_favoritoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var entidade = await context.Tabela21Favorito.FindAsync(id);
            if (entidade is null)
                return (false, "Favorito não encontrado.", 404);

            context.Tabela21Favorito.Remove(entidade);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Favorito removido com sucesso!", 200) :
                (false, "Erro ao remover favorito.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






