using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E15_municipio;

public class RemoverMunicipioRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb15_municipioModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var municipio = await context.Tabela15Municipio.FindAsync(id);
            if (municipio is null)
                return (false, "Município não encontrado.", 404);

            context.Tabela15Municipio.Remove(municipio);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Município removido com sucesso!", 200) :
                (false, "Erro ao remover município.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






