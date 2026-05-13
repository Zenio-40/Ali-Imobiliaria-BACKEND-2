using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E06_cliente;

public class ActualizarClienteRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb06_clienteModel>
{
    public async Task<(tb06_clienteModel? dado, string mensagem, int codigo)> ActualizarAsync(tb06_clienteModel model)
    {
        try
        {
            var cliente = await context.Tabela06Cliente.FindAsync(model.Id);
            if (cliente is null)
                return (null, "Cliente não encontrado.", 404);

            cliente.Nome = model.Nome;
            cliente.Estado = model.Estado;
            cliente.Idtb02_perfilModel = model.Idtb02_perfilModel;

            return await context.SaveChangesAsync() > 0 ?
                (cliente, "Cliente actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar cliente.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



