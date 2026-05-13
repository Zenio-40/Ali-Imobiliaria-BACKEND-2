using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E01_permissao;

public class RemoverPermissaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb01_permissaoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var permissao = await context.Tabela01Permissao.FindAsync(id);
            if (permissao is null)
                return (false, "Permissão não encontrada.", 404);

            context.Tabela01Permissao.Remove(permissao);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Permissão removida com sucesso!", 200) :
                (false, "Erro ao remover permissão.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}





