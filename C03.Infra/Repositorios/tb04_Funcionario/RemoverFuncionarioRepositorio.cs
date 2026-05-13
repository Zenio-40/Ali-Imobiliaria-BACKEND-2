using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E04_funcionario;

public class RemoverFuncionarioRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb04_funcionarioModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
   {
        try
        {
            var funcionario = await context.Tabela04Funcinario.FindAsync(id);
            if(funcionario is null)
                return (false, "Funcionário não encontrado.", 404);

            context.Tabela04Funcinario.Remove(funcionario);
            return await context.SaveChangesAsync() > 0 ?
            (true, "Funcionário removido com sucesso!", 200) :
            (false, "Erro ao remover funcionário.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
    }
}
}





