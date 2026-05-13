using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E04_funcionario;

public class PesquisarTodosFuncionarioRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb04_funcionarioModel>
{
    public async Task<(IEnumerable<tb04_funcionarioModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela04Funcinario
                .Include(f => f.Telefone)
                .Include(f => f.Perfil)
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Funcionários encontrados com sucesso!", 200) :
                (Array.Empty<tb04_funcionarioModel>(), "Nenhum funcionário encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}


