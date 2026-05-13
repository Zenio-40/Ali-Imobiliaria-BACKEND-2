using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E20_solicitacao;

public class PesquisarPorIdSolicitacaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb20_solicitacaoModel>
{
    public async Task<(tb20_solicitacaoModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var entidade = await context.Tabela20Solicitacao
                .Include(s => s.Cliente)
                .Include(s => s.Imovel)
                .Include(s => s.EstadoSolicitacao)
                .FirstOrDefaultAsync(s => s.Id == id);
            return entidade is not null ?
                (entidade, "Solicitação encontrada com sucesso!", 200) :
                (null, "Solicitação não encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



