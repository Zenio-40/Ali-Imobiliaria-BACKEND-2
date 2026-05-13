using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E12_foto;

public class ActualizarFotoRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb12_fotoModel>
{
    public async Task<(tb12_fotoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb12_fotoModel model)
    {
        try
        {
            var foto = await context.Tabela12Foto.FindAsync(model.Id);
            if (foto is null)
                return (null, "Foto não encontrada.", 404);

            foto.Foto = model.Foto;
            foto.tb11_imovelModel = model.tb11_imovelModel;
            foto.tb09_tipo_imovel = model.tb09_tipo_imovel;

            return await context.SaveChangesAsync() > 0 ?
                (foto, "Foto actualizada com sucesso!", 200) :
                (null, "Erro ao actualizar foto.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



