using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E09_tipo_imovel;

public class PesquisarPorIdTipoImovelRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb09_tipo_imovelModel>
{
    public async Task<(tb09_tipo_imovelModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var tipoImovel = await context.Tabela09TipolaImovel.FindAsync(id);
            return tipoImovel is not null ?
                (tipoImovel, "Tipo de imóvel encontrado com sucesso!", 200) :
                (null, "Tipo de imóvel não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



