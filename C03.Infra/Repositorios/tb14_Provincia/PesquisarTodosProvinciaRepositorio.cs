using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E14_provincia;

public class PesquisarTodosProvinciaRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb14_provinciaModel>
{
    public async Task<(IEnumerable<tb14_provinciaModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela14Pronvincia
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Províncias encontradas com sucesso!", 200) :
                (Array.Empty<tb14_provinciaModel>(), "Nenhuma província encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



