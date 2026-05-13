using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E17_endereco;

public class RemoverEnderecoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb17_enderecoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var entidade = await context.Tabela17Enderco.FindAsync(id);
            if (entidade is null)
                return (false, "Endereço não encontrado.", 404);

            context.Tabela17Enderco.Remove(entidade);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Endereço removido com sucesso!", 200) :
                (false, "Erro ao remover endereço.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






