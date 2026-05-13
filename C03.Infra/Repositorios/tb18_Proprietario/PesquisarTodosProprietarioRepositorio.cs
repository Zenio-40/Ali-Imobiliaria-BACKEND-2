using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E18_proprietario;

public class PesquisarTodosProprietarioRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb18_proprietarioModel>
{
    public async Task<(IEnumerable<tb18_proprietarioModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela18Proprietario
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Proprietários encontrados com sucesso!", 200) :
                (Array.Empty<tb18_proprietarioModel>(), "Nenhum proprietário encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



