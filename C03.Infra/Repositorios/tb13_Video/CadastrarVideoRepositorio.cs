using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E13_video;

public class CadastrarVideoRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb13_videoModel>
{
    public async Task<(tb13_videoModel? dado, string mensagem, int codigo)> CadastrarAsync(tb13_videoModel model)
    {
        try
        {
            await context.Tabela13Video.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Vídeo cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar vídeo.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



