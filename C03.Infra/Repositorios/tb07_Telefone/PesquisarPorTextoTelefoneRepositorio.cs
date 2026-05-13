using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E07_telefone;

public class PesquisarPorTextoTelefoneRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb07_telefoneModel>
{
    public async Task<(IEnumerable<tb07_telefoneModel>? dados, string mensagem, int codigo)> PesquisarPorTextoAsync(string texto, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela07Telefone
                .Where(t => t.Numero.Contains(texto))
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Telefones encontrados com sucesso!", 200) :
                (Array.Empty<tb07_telefoneModel>(), "Nenhum telefone encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



