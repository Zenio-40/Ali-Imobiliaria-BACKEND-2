using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E07_telefone;

public class CadastrarTelefoneRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb07_telefoneModel>
{
    public async Task<(tb07_telefoneModel? dado, string mensagem, int codigo)> CadastrarAsync(tb07_telefoneModel model)
    {
        try
        {
            await context.Tabela07Telefone.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Telefone cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar telefone.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



