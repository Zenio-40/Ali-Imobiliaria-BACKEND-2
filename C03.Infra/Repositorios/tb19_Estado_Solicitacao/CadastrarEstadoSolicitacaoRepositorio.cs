using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E19_estado_solicitacao;

public class CadastrarEstadoSolicitacaoRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb19_estado_solicitacaoModel>
{
    public async Task<(tb19_estado_solicitacaoModel? dado, string mensagem, int codigo)> CadastrarAsync(tb19_estado_solicitacaoModel model)
    {
        try
        {
            await context.Tabela19EstadoSolicitacao.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Estado de solicitação cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar estado de solicitação.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



