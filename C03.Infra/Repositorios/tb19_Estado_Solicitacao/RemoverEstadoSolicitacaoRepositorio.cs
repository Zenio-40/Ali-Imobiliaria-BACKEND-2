using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E19_estado_solicitacao;

public class RemoverEstadoSolicitacaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb19_estado_solicitacaoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var entidade = await context.Tabela19EstadoSolicitacao.FindAsync(id);
            if (entidade is null)
                return (false, "Estado de solicitação não encontrado.", 404);

            context.Tabela19EstadoSolicitacao.Remove(entidade);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Estado de solicitação removido com sucesso!", 200) :
                (false, "Erro ao remover estado de solicitação.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






