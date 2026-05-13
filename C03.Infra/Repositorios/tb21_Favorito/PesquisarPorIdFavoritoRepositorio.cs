using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E21_favorito;

public class PesquisarPorIdFavoritoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb21_favoritoModel>
{
    public async Task<(tb21_favoritoModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var entidade = await context.Tabela21Favorito.FindAsync(id);
            return entidade is not null ?
                (entidade, "Favorito encontrado com sucesso!", 200) :
                (null, "Favorito não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



