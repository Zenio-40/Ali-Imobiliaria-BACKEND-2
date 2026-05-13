using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E10_tipologia;

public class ActualizarTipologiaRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb10_tipologiaModel>
{
    public async Task<(tb10_tipologiaModel? dado, string mensagem, int codigo)> ActualizarAsync(tb10_tipologiaModel model)
    {
        try
        {
            var tipologia = await context.Tabela10Tipologia.FindAsync(model.Id);
            if (tipologia is null)
                return (null, "Tipologia não encontrada.", 404);

            tipologia.Descricao = model.Descricao;

            return await context.SaveChangesAsync() > 0 ?
                (tipologia, "Tipologia actualizada com sucesso!", 200) :
                (null, "Erro ao actualizar tipologia.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



