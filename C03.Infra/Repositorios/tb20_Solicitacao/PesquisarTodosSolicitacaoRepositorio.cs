using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E20_solicitacao;

public class PesquisarTodosSolicitacaoRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb20_solicitacaoModel>
{
    public async Task<(IEnumerable<tb20_solicitacaoModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela20Solicitacao
                .Include(s => s.Cliente)
                .Include(s => s.Imovel)
                .Include(s => s.EstadoSolicitacao)
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Solicitações encontradas com sucesso!", 200) :
                (Array.Empty<tb20_solicitacaoModel>(), "Nenhuma solicitação encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



