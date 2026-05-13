using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E16_bairro;

public class CadastrarBairroRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb16_bairroModel>
{
    public async Task<(tb16_bairroModel? dado, string mensagem, int codigo)> CadastrarAsync(tb16_bairroModel model)
    {
        try
        {
            await context.Tabela16Bairro.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Bairro cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar bairro.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



