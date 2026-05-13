using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E16_bairro;

public class PesquisarTodosBairroRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb16_bairroModel>
{
    public async Task<(IEnumerable<tb16_bairroModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela16Bairro
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Bairros encontrados com sucesso!", 200) :
                (Array.Empty<tb16_bairroModel>(), "Nenhum bairro encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



