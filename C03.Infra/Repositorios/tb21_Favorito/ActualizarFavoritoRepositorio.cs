using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E21_favorito;

public class ActualizarFavoritoRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb21_favoritoModel>
{
    public async Task<(tb21_favoritoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb21_favoritoModel model)
    {
        try
        {
            var entidade = await context.Tabela21Favorito.FindAsync(model.Id);
            if (entidade is null)
                return (null, "Favorito não encontrado.", 404);

            entidade.tb06_clienteModel = model.tb06_clienteModel;
            entidade.tb11_imovelModel = model.tb11_imovelModel;

            return await context.SaveChangesAsync() > 0 ?
                (entidade, "Favorito actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar favorito.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



