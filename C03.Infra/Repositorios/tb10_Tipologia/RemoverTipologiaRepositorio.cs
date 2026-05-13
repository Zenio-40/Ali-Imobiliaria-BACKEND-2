using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E10_tipologia;

public class RemoverTipologiaRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb10_tipologiaModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var tipologia = await context.Tabela10Tipologia.FindAsync(id);
            if (tipologia is null)
                return (false, "Tipologia não encontrada.", 404);

            context.Tabela10Tipologia.Remove(tipologia);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Tipologia removida com sucesso!", 200) :
                (false, "Erro ao remover tipologia.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






