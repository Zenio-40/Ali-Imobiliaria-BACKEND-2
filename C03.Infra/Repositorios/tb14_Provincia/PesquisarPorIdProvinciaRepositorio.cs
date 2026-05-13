using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E14_provincia;

public class PesquisarPorIdProvinciaRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb14_provinciaModel>
{
    public async Task<(tb14_provinciaModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var provincia = await context.Tabela14Pronvincia.FindAsync(id);
            return provincia is not null ?
                (provincia, "Província encontrada com sucesso!", 200) :
                (null, "Província não encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



