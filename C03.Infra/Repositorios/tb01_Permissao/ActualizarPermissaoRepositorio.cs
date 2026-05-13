using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E01_permissao;

public class ActualizarPermissaoRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb01_permissaoModel>
{
    public async Task<(tb01_permissaoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb01_permissaoModel model)
    {
        try
        {
            var permissao = await context.Tabela01Permissao.FindAsync(model.id);
            if (permissao is null)
                return (null, "Permissão não encontrada.", 404);

            permissao.Descricao = model.Descricao;

            return await context.SaveChangesAsync() > 0 ?
                (permissao, "Permissão actualizada com sucesso!", 200) :
                (null, "Erro ao actualizar permissão.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}


