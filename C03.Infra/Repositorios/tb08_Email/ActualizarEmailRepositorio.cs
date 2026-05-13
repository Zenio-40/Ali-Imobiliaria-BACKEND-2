using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E08_email;

public class ActualizarEmailRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb08_emailModel>
{
    public async Task<(tb08_emailModel? dado, string mensagem, int codigo)> ActualizarAsync(tb08_emailModel model)
    {
        try
        {
            var email = await context.Tabela08Email.FindAsync(model.Id);
            if (email is null)
                return (null, "Email não encontrado.", 404);

            email.Endereco = model.Endereco;
            email.tb06_clienteModel = model.tb06_clienteModel;
            email.tb04_funcionarioModel = model.tb04_funcionarioModel;

            return await context.SaveChangesAsync() > 0 ?
                (email, "Email actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar email.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



