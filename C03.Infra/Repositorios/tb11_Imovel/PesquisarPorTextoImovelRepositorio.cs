using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E11_imovel;

public class PesquisarPorTextoImovelRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb11_imovelModel>
{
    public async Task<(IEnumerable<tb11_imovelModel>? dados, string mensagem, int codigo)> PesquisarPorTextoAsync(string texto, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela11Imovel
                .Where(i => i.Descricao.Contains(texto) || i.Preco.ToString().Contains(texto))
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



