using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E15_municipio;

public class CadastrarMunicipioRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb15_municipioModel>
{
    public async Task<(tb15_municipioModel? dado, string mensagem, int codigo)> CadastrarAsync(tb15_municipioModel model)
    {
        try
        {
            await context.Tabela15Municipio.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Município cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar município.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



