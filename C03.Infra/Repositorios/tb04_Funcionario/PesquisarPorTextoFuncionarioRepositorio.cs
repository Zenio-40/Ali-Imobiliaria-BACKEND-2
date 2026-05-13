using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E04_funcionario;

public class PesquisarPorTextoFuncionarioRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb04_funcionarioModel>
{
    public async Task<(IEnumerable<tb04_funcionarioModel>? dados, string mensagem, int codigo)> PesquisarPorTextoAsync(string texto, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela04Funcinario
            .Where(f => f.Telefone.Any (t => t.Numero.Contains(texto)))
            .Skip((pagina - 1) * quantidade)
            .Take(quantidade)
            .ToListAsync();

            return dados.Count > 0 ?
            (dados, "Funcionários encontrados com sucesso!", 200) :
            ([], "Nenhum funcionário encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}


