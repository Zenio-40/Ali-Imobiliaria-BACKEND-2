using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E18_proprietario;

public class RemoverProprietarioRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb18_proprietarioModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var entidade = await context.Tabela18Proprietario.FindAsync(id);
            if (entidade is null)
                return (false, "Proprietário não encontrado.", 404);

            context.Tabela18Proprietario.Remove(entidade);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Proprietário removido com sucesso!", 200) :
                (false, "Erro ao remover proprietário.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






