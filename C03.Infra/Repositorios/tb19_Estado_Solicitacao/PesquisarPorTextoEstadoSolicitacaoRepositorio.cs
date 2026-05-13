using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E19_estado_solicitacao;

public class PesquisarPorTextoEstadoSolicitacaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb19_estado_solicitacaoModel>
{
    public async Task<(IEnumerable<tb19_estado_solicitacaoModel>? dados, string mensagem, int codigo)> PesquisarPorTextoAsync(string texto, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela19EstadoSolicitacao
                .Where(s => s.Nome.Contains(texto) || s.Descricao.Contains(texto))
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Estados de solicitação encontrados com sucesso!", 200) :
                (Array.Empty<tb19_estado_solicitacaoModel>(), "Nenhum estado de solicitação encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



