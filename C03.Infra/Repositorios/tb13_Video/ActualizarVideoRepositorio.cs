using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E13_video;

public class ActualizarVideoRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb13_videoModel>
{
    public async Task<(tb13_videoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb13_videoModel model)
    {
        try
        {
            var video = await context.Tabela13Video.FindAsync(model.Id);
            if (video is null)
                return (null, "Vídeo não encontrado.", 404);

            video.Video = model.Video;
            video.tb11_imovelModel = model.tb11_imovelModel;
            video.tb09_tipo_imovel = model.tb09_tipo_imovel;

            return await context.SaveChangesAsync() > 0 ?
                (video, "Vídeo actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar vídeo.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



