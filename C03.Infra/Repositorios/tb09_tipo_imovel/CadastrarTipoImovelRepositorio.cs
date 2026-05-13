using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E09_tipo_imovel;

public class CadastrarTipoImovelRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb09_tipo_imovelModel>
{
    public async Task<(tb09_tipo_imovelModel? dado, string mensagem, int codigo)> CadastrarAsync(tb09_tipo_imovelModel model)
    {
        try
        {
            await context.Tabela09TipolaImovel.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Tipo de imóvel cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar tipo de imóvel.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



