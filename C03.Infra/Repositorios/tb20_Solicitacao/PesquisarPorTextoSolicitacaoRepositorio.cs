using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E20_solicitacao;

public class PesquisarPorTextoSolicitacaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb20_solicitacaoModel>
{
    public async Task<(IEnumerable<tb20_solicitacaoModel>? dados, string mensagem, int codigo)> PesquisarPorTextoAsync(string texto, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela20Solicitacao
                .Where(s => s.Data.ToString().Contains(texto) || s.tb06_clienteModel.ToString().Contains(texto) || s.tb11_imovelModel.ToString().Contains(texto) || s.tb19_estado_solicitacaoModel.ToString().Contains(texto))
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



