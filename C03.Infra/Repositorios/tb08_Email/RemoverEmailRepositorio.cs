using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E08_email;

public class RemoverEmailRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb08_emailModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var email = await context.Tabela08Email.FindAsync(id);
            if (email is null)
                return (false, "Email não encontrado.", 404);

            context.Tabela08Email.Remove(email);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Email removido com sucesso!", 200) :
                (false, "Erro ao remover email.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






