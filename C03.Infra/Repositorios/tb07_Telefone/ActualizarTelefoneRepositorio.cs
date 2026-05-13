using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E07_telefone;

public class ActualizarTelefoneRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb07_telefoneModel>
{
    public async Task<(tb07_telefoneModel? dado, string mensagem, int codigo)> ActualizarAsync(tb07_telefoneModel model)
    {
        try
        {
            var telefone = await context.Tabela07Telefone.FindAsync(model.Id);
            if (telefone is null)
                return (null, "Telefone não encontrado.", 404);

            telefone.Numero = model.Numero;
            telefone.tb06_clienteModel = model.tb06_clienteModel;
            telefone.tb04_funcionarioModel = model.tb04_funcionarioModel;

            return await context.SaveChangesAsync() > 0 ?
                (telefone, "Telefone actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar telefone.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



