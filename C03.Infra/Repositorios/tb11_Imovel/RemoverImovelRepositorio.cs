using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E11_imovel;

public class RemoverImovelRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb11_imovelModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var imovel = await context.Tabela11Imovel.FindAsync(id);
            if (imovel is null)
                return (false, "Imóvel não encontrado.", 404);

            context.Tabela11Imovel.Remove(imovel);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Imóvel removido com sucesso!", 200) :
                (false, "Erro ao remover imóvel.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






