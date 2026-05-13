using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E09_tipo_imovel;

public class RemoverTipoImovelRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb09_tipo_imovelModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var tipoImovel = await context.Tabela09TipolaImovel.FindAsync(id);
            if (tipoImovel is null)
                return (false, "Tipo de imóvel não encontrado.", 404);

            context.Tabela09TipolaImovel.Remove(tipoImovel);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Tipo de imóvel removido com sucesso!", 200) :
                (false, "Erro ao remover tipo de imóvel.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






