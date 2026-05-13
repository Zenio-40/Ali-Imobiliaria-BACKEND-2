using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E02_perfil;

public class PesquisarTodosPerfilRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb02_perfilModel>
{
    public async Task<(IEnumerable<tb02_perfilModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela02Perfil
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Perfis encontrados com sucesso!", 200) :
                (Array.Empty<tb02_perfilModel>(), "Nenhum perfil encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



