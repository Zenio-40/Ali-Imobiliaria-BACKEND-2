using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E20_solicitacao;

public class RemoverSolicitacaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb20_solicitacaoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var entidade = await context.Tabela20Solicitacao.FindAsync(id);
            if (entidade is null)
                return (false, "Solicitação não encontrada.", 404);

            context.Tabela20Solicitacao.Remove(entidade);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Solicitação removida com sucesso!", 200) :
                (false, "Erro ao remover solicitação.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






