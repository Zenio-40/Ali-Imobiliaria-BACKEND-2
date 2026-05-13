using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E17_endereco;

public class ActualizarEnderecoRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb17_enderecoModel>
{
    public async Task<(tb17_enderecoModel? dado, string mensagem, int codigo)> ActualizarAsync(tb17_enderecoModel model)
    {
        try
        {
            var entidade = await context.Tabela17Enderco.FindAsync(model.Id);
            if (entidade is null)
                return (null, "Endereço não encontrado.", 404);

            entidade.tb11_imovelModel = model.tb11_imovelModel;
            entidade.tb09_tipo_imovelModel = model.tb09_tipo_imovelModel;
            entidade.tb16_bairroModel = model.tb16_bairroModel;
            entidade.Nome = model.Nome;

            return await context.SaveChangesAsync() > 0 ?
                (entidade, "Endereço actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar endereço.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



