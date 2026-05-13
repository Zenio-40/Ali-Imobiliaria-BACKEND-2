using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E16_bairro;

public class ActualizarBairroRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb16_bairroModel>
{
    public async Task<(tb16_bairroModel? dado, string mensagem, int codigo)> ActualizarAsync(tb16_bairroModel model)
    {
        try
        {
            var entidade = await context.Tabela16Bairro.FindAsync(model.Id);
            if (entidade is null)
                return (null, "Bairro não encontrado.", 404);

            entidade.tb15_municipioModel = model.tb15_municipioModel;
            entidade.Nome = model.Nome;

            return await context.SaveChangesAsync() > 0 ?
                (entidade, "Bairro actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar bairro.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



