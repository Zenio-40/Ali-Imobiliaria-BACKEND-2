using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E04_funcionario;

public class ActualizarFuncionarioRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb04_funcionarioModel>
{
    public async Task<(tb04_funcionarioModel? dado, string mensagem, int codigo)> ActualizarAsync(tb04_funcionarioModel model)
    {
        try
        {
            var funcionario = await context.Tabela04Funcinario.FindAsync(model.Id);
            if(funcionario is null)
                return (null, "Funcionário não encontrado.", 404);

                funcionario.Nome = model.Nome;
                funcionario.Telefone = model.Telefone;
                funcionario.Estado = model.Estado;
                funcionario.Credencial = model.Credencial;
                funcionario.Email = model.Email;
                funcionario.Perfil = model.Perfil;

                return await context.SaveChangesAsync() > 0 ?
                (funcionario, "Funcionário actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar funcionário.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}


