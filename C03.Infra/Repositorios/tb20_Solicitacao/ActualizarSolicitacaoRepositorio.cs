using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E20_solicitacao;

public class ActualizarSolicitacaoRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb20_solicitacaoModel>
{
    public async Task<(tb20_solicitacaoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb20_solicitacaoModel model)
    {
        try
        {
            var entidade = await context.Tabela20Solicitacao.FindAsync(model.Id);
            if (entidade is null)
                return (null, "Solicitação não encontrada.", 404);

            entidade.tb06_clienteModel = model.tb06_clienteModel;
            entidade.tb11_imovelModel = model.tb11_imovelModel;
            entidade.tb19_estado_solicitacaoModel = model.tb19_estado_solicitacaoModel;
            entidade.Data = model.Data;

            return await context.SaveChangesAsync() > 0 ?
                (entidade, "Solicitação actualizada com sucesso!", 200) :
                (null, "Erro ao actualizar solicitação.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



