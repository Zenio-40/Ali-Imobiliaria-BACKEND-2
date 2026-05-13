using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E01_permissao;

public class CadastrarPermissaoRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb01_permissaoModel>
{
    public async Task<(tb01_permissaoModel? dado, string mensagem, int codigo)> CadastrarAsync(tb01_permissaoModel model)
    {
        try
        {
            await context.Tabela01Permissao.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Permissão cadastrada com sucesso!", 201) :
                (null, "Erro ao cadastrar permissão.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}


