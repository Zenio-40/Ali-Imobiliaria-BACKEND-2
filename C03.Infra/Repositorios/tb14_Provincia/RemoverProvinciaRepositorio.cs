using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E14_provincia;

public class RemoverProvinciaRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb14_provinciaModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var provincia = await context.Tabela14Pronvincia.FindAsync(id);
            if (provincia is null)
                return (false, "Província não encontrada.", 404);

            context.Tabela14Pronvincia.Remove(provincia);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Província removida com sucesso!", 200) :
                (false, "Erro ao remover província.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






