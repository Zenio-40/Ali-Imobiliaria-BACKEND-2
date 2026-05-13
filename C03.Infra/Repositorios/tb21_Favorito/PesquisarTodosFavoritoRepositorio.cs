using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E21_favorito;

public class PesquisarTodosFavoritoRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb21_favoritoModel>
{
    public async Task<(IEnumerable<tb21_favoritoModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela21Favorito
                .Include(f => f.Imovel)
                    .ThenInclude(i => i.TipoImovel)
                .Include(f => f.Imovel)
                    .ThenInclude(i => i.Tipologia)
                .Include(f => f.Imovel)
                    .ThenInclude(i => i.Funcionario)
                .Include(f => f.Imovel)
                    .ThenInclude(i => i.ProprietarioModel)
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Favoritos encontrados com sucesso!", 200) :
                (Array.Empty<tb21_favoritoModel>(), "Nenhum favorito encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



