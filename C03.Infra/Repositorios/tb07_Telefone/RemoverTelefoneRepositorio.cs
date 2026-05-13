using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E07_telefone;

public class RemoverTelefoneRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb07_telefoneModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var telefone = await context.Tabela07Telefone.FindAsync(id);
            if (telefone is null)
                return (false, "Telefone não encontrado.", 404);

            context.Tabela07Telefone.Remove(telefone);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Telefone removido com sucesso!", 200) :
                (false, "Erro ao remover telefone.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






