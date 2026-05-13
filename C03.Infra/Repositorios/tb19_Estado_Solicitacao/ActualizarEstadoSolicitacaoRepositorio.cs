using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E19_estado_solicitacao;

public class ActualizarEstadoSolicitacaoRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb19_estado_solicitacaoModel>
{
    public async Task<(tb19_estado_solicitacaoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb19_estado_solicitacaoModel model)
    {
        try
        {
            var entidade = await context.Tabela19EstadoSolicitacao.FindAsync(model.Id);
            if (entidade is null)
                return (null, "Estado de solicitação não encontrado.", 404);

            entidade.Nome = model.Nome;
            entidade.Descricao = model.Descricao;

            return await context.SaveChangesAsync() > 0 ?
                (entidade, "Estado de solicitação actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar estado de solicitação.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



