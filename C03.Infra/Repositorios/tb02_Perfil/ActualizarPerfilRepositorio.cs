using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E02_perfil;

public class ActualizarPerfilRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb02_perfilModel>
{
    public async Task<(tb02_perfilModel? dado, string mensagem, int codigo)> ActualizarAsync(tb02_perfilModel model)
    {
        try
        {
            var perfil = await context.Tabela02Perfil.FindAsync(model.id);
            if (perfil is null)
                return (null, "Perfil não encontrado.", 404);

            perfil.Descricao = model.Descricao;

            return await context.SaveChangesAsync() > 0 ?
                (perfil, "Perfil actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar perfil.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



