using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E09_tipo_imovel;

public class ActualizarTipoImovelRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb09_tipo_imovelModel>
{
    public async Task<(tb09_tipo_imovelModel? dado, string mensagem, int codigo)> ActualizarAsync(tb09_tipo_imovelModel model)
    {
        try
        {
            var tipoImovel = await context.Tabela09TipolaImovel.FindAsync(model.Id);
            if (tipoImovel is null)
                return (null, "Tipo de imóvel não encontrado.", 404);

            tipoImovel.Descricao = model.Descricao;

            return await context.SaveChangesAsync() > 0 ?
                (tipoImovel, "Tipo de imóvel actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar tipo de imóvel.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



