using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E10_tipologia;

public class PesquisarPorIdTipologiaRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb10_tipologiaModel>
{
    public async Task<(tb10_tipologiaModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var tipologia = await context.Tabela10Tipologia.FindAsync(id);
            return tipologia is not null ?
                (tipologia, "Tipologia encontrada com sucesso!", 200) :
                (null, "Tipologia não encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



