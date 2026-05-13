using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E03_perfil_permissao;

public class RemoverPerfilPermissaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb03_perfiL_permissaoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var perfilPermissao = await context.Tabela03PerfilPermissao.FindAsync(id);
            if (perfilPermissao is null)
                return (false, "Perfil Permissão não encontrado.", 404);

            context.Tabela03PerfilPermissao.Remove(perfilPermissao);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Perfil Permissão removido com sucesso!", 200) :
                (false, "Erro ao remover perfil permissão.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






