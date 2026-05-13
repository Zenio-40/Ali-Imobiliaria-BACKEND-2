using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E03_perfil_permissao;

public class ActualizarPerfilPermissaoRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb03_perfiL_permissaoModel>
{
    public async Task<(tb03_perfiL_permissaoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb03_perfiL_permissaoModel model)
    {
        try
        {
            var perfilPermissao = await context.Tabela03PerfilPermissao.FindAsync(model.id);
            if (perfilPermissao is null)
                return (null, "Perfil Permissão não encontrado.", 404);

            perfilPermissao.Idtb02_perfilModel = model.Idtb02_perfilModel;
            perfilPermissao.Idtb01_permissaoModel = model.Idtb01_permissaoModel;
            perfilPermissao.Estado = model.Estado;

            return await context.SaveChangesAsync() > 0 ?
                (perfilPermissao, "Perfil Permissão actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar perfil permissão.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



