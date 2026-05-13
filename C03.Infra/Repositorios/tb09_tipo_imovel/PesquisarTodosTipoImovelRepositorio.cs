using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E09_tipo_imovel;

public class PesquisarTodosTipoImovelRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb09_tipo_imovelModel>
{
    public async Task<(IEnumerable<tb09_tipo_imovelModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela09TipolaImovel
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Tipos de imóvel encontrados com sucesso!", 200) :
                (Array.Empty<tb09_tipo_imovelModel>(), "Nenhum tipo de imóvel encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



