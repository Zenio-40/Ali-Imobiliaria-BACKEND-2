using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E11_imovel;

public class PesquisarPorIdImovelRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb11_imovelModel>
{
    public async Task<(tb11_imovelModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var imovel = await context.Tabela11Imovel
                .Include(i => i.Funcionario)
                .Include(i => i.TipoImovel)
                .Include(i => i.Tipologia)
                .Include(i => i.ProprietarioModel)
                .Include(i => i.Foto)
                .Include(i => i.Video)
                .Include(i => i.Endereco)
                    .ThenInclude(e => e.Bairro)
                        .ThenInclude(b => b.Municipio)
                            .ThenInclude(m => m.Provincia)
                .FirstOrDefaultAsync(i => i.Id == id);
            return imovel is not null ?
                (imovel, "Imóvel encontrado com sucesso!", 200) :
                (null, "Imóvel não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



