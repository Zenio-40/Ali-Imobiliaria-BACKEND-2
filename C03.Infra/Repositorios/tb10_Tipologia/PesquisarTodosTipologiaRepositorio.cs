using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E10_tipologia;

public class PesquisarTodosTipologiaRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb10_tipologiaModel>
{
    public async Task<(IEnumerable<tb10_tipologiaModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela10Tipologia
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Tipologias encontradas com sucesso!", 200) :
                (Array.Empty<tb10_tipologiaModel>(), "Nenhuma tipologia encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



