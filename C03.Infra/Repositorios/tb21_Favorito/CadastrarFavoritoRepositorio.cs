using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E21_favorito;

public class CadastrarFavoritoRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb21_favoritoModel>
{
    public async Task<(tb21_favoritoModel? dado, string mensagem, int codigo)> CadastrarAsync(tb21_favoritoModel model)
    {
        try
        {
            await context.Tabela21Favorito.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Favorito cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar favorito.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



