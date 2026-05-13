using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E13_video;

public class RemoverVideoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb13_videoModel>
{
    public async Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id)
    {
        try
        {
            var video = await context.Tabela13Video.FindAsync(id);
            if (video is null)
                return (false, "Vídeo não encontrado.", 404);

            context.Tabela13Video.Remove(video);
            return await context.SaveChangesAsync() > 0 ?
                (true, "Vídeo removido com sucesso!", 200) :
                (false, "Erro ao remover vídeo.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.ToString(), 500);
        }
    }
}






