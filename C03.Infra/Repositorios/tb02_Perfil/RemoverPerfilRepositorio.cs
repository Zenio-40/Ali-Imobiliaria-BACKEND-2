using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E02_perfil;

public class RemoverPerfilRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb02_perfilModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var perfil = await context.Tabela02Perfil.FindAsync(id);
            if (perfil is null)
                return (false, "Perfil não encontrado.", 404);

            context.Tabela02Perfil.Remove(perfil);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Perfil removido com sucesso!", 200) :
                (false, "Erro ao remover perfil.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}
