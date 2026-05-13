using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E05_credencial_acesso;

public class ActualizarCredencialRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb05_credencial_acessoModel>
{
    public async Task<(tb05_credencial_acessoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb05_credencial_acessoModel model)
    {
        try
        {
            var credencial = await context.Tabela05Credencial.FindAsync(model.Id);
            if (credencial is null)
                return (null, "Credencial não encontrada.", 404);

            credencial.Senha_hash = model.Senha_hash;
            credencial.Senha_salt = model.Senha_salt;

            return await context.SaveChangesAsync() > 0 ?
                (credencial, "Credencial actualizada com sucesso!", 200) :
                (null, "Erro ao actualizar credencial.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



