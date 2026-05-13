using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E18_proprietario;

public class ActualizarProprietarioRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb18_proprietarioModel>
{
    public async Task<(tb18_proprietarioModel? dado, string mensagem, int codigo)> ActualizarAsync(tb18_proprietarioModel model)
    {
        try
        {
            var entidade = await context.Tabela18Proprietario.FindAsync(model.Id);
            if (entidade is null)
                return (null, "Proprietário não encontrado.", 404);

            entidade.tb09_tipo_imovelModel = model.tb09_tipo_imovelModel;
            entidade.Nome = model.Nome;
            entidade.Telefone = model.Telefone;

            return await context.SaveChangesAsync() > 0 ?
                (entidade, "Proprietário actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar proprietário.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



