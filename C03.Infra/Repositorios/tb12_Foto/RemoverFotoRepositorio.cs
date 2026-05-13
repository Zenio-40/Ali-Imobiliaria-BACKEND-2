using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E12_foto;

public class RemoverFotoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb12_fotoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var foto = await context.Tabela12Foto.FindAsync(id);
            if (foto is null)
                return (false, "Foto não encontrada.", 404);

            context.Tabela12Foto.Remove(foto);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Foto removida com sucesso!", 200) :
                (false, "Erro ao remover foto.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






