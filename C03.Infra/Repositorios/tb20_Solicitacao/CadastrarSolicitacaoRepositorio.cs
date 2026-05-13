using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E20_solicitacao;

public class CadastrarSolicitacaoRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb20_solicitacaoModel>
{
    public async Task<(tb20_solicitacaoModel? dado, string mensagem, int codigo)> CadastrarAsync(tb20_solicitacaoModel model)
    {
        try
        {
            await context.Tabela20Solicitacao.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Solicitação cadastrada com sucesso!", 201) :
                (null, "Erro ao cadastrar solicitação.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



