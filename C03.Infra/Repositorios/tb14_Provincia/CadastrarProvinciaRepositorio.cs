using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E14_provincia;

public class CadastrarProvinciaRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb14_provinciaModel>
{
    public async Task<(tb14_provinciaModel? dado, string mensagem, int codigo)> CadastrarAsync(tb14_provinciaModel model)
    {
        try
        {
            await context.Tabela14Pronvincia.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Província cadastrada com sucesso!", 201) :
                (null, "Erro ao cadastrar província.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



