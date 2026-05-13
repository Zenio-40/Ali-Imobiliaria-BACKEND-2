using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E14_provincia;

public class ActualizarProvinciaRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb14_provinciaModel>
{
    public async Task<(tb14_provinciaModel? dado, string mensagem, int codigo)> ActualizarAsync(tb14_provinciaModel model)
    {
        try
        {
            var provincia = await context.Tabela14Pronvincia.FindAsync(model.Id);
            if (provincia is null)
                return (null, "Província não encontrada.", 404);

            provincia.Nome = model.Nome;

            return await context.SaveChangesAsync() > 0 ?
                (provincia, "Província actualizada com sucesso!", 200) :
                (null, "Erro ao actualizar província.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



