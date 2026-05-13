using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E10_tipologia;

public class CadastrarTipologiaRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb10_tipologiaModel>
{
    public async Task<(tb10_tipologiaModel? dado, string mensagem, int codigo)> CadastrarAsync(tb10_tipologiaModel model)
    {
        try
        {
            await context.Tabela10Tipologia.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Tipologia cadastrada com sucesso!", 201) :
                (null, "Erro ao cadastrar tipologia.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



