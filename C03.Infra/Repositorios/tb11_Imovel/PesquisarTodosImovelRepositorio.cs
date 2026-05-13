using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E11_imovel;

public class PesquisarTodosImovelRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb11_imovelModel>
{
    public async Task<(IEnumerable<tb11_imovelModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela11Imovel
                .Include(i => i.Funcionario)
                .Include(i => i.TipoImovel)
                .Include(i => i.Tipologia)
                .Include(i => i.ProprietarioModel)
                .Include(i => i.Foto)
                .Include(i => i.Video)
                .Include(i => i.Endereco)
                    .ThenInclude(e => e.Bairro)
                        .ThenInclude(b => b.Municipio)
                            .ThenInclude(m => m.Provincia)
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Imóveis encontrados com sucesso!", 200) :
                (Array.Empty<tb11_imovelModel>(), "Nenhum imóvel encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



