using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E06_cliente;

public class RemoverClienteRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb06_clienteModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var cliente = await context.Tabela06Cliente.FindAsync(id);
            if (cliente is null)
                return (false, "Cliente não encontrado.", 404);

            context.Tabela06Cliente.Remove(cliente);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Cliente removido com sucesso!", 200) :
                (false, "Erro ao remover cliente.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






