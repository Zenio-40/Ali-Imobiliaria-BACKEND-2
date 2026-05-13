using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E05_credencial_acesso;

public class RemoverCredencialRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb05_credencial_acessoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var credencial = await context.Tabela05Credencial.FindAsync(id);
            if (credencial is null)
                return (false, "Credencial não encontrada.", 404);

            context.Tabela05Credencial.Remove(credencial);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Credencial removida com sucesso!", 200) :
                (false, "Erro ao remover credencial.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






