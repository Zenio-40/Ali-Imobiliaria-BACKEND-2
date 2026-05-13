using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E16_bairro;

public class RemoverBairroRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb16_bairroModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var entidade = await context.Tabela16Bairro.FindAsync(id);
            if (entidade is null)
                return (false, "Bairro não encontrado.", 404);

            context.Tabela16Bairro.Remove(entidade);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Bairro removido com sucesso!", 200) :
                (false, "Erro ao remover bairro.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






