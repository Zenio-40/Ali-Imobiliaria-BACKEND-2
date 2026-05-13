using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E01_permissao;

public class PesquisarTodosPermissaoRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb01_permissaoModel>
{
    public async Task<(IEnumerable<tb01_permissaoModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela01Permissao
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Permissões encontradas com sucesso!", 200) :
                (Array.Empty<tb01_permissaoModel>(), "Nenhuma permissão encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}


