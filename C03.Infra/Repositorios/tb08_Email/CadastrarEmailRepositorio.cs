using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E08_email;

public class CadastrarEmailRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb08_emailModel>
{
    public async Task<(tb08_emailModel? dado, string mensagem, int codigo)> CadastrarAsync(tb08_emailModel model)
    {
        try
        {
            await context.Tabela08Email.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Email cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar email.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



