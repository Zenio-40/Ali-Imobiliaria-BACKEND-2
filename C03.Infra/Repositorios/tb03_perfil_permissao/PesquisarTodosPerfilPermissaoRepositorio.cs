using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E03_perfil_permissao;

public class PesquisarTodosPerfilPermissaoRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb03_perfiL_permissaoModel>
{
    public async Task<(IEnumerable<tb03_perfiL_permissaoModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela03PerfilPermissao
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Perfis Permissão encontrados com sucesso!", 200) :
                (Array.Empty<tb03_perfiL_permissaoModel>(), "Nenhum perfil permissão encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



