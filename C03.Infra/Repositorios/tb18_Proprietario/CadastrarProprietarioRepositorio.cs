using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E18_proprietario;

public class CadastrarProprietarioRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb18_proprietarioModel>
{
    public async Task<(tb18_proprietarioModel? dado, string mensagem, int codigo)> CadastrarAsync(tb18_proprietarioModel model)
    {
        try
        {
            await context.Tabela18Proprietario.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Proprietário cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar proprietário.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



