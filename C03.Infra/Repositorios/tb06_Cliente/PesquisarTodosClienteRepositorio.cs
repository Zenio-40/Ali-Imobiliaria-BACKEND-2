using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E06_cliente;

public class PesquisarTodosClienteRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb06_clienteModel>
{
    public async Task<(IEnumerable<tb06_clienteModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela06Cliente
                .Include(c => c.Perfil)
                .Include(c => c.Telefone)
                .Include(c => c.Email)
                .Include(c => c.Credencial)
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Clientes encontrados com sucesso!", 200) :
                (Array.Empty<tb06_clienteModel>(), "Nenhum cliente encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



