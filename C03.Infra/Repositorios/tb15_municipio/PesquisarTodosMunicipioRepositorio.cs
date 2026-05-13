using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E15_municipio;

public class PesquisarTodosMunicipioRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb15_municipioModel>
{
    public async Task<(IEnumerable<tb15_municipioModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela15Municipio
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Municípios encontrados com sucesso!", 200) :
                (Array.Empty<tb15_municipioModel>(), "Nenhum município encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



