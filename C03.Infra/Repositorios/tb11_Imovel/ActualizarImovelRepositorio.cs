using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E11_imovel;

public class ActualizarImovelRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb11_imovelModel>
{
    public async Task<(tb11_imovelModel? dado, string mensagem, int codigo)> ActualizarAsync(tb11_imovelModel model)
    {
        try
        {
            var imovel = await context.Tabela11Imovel.FindAsync(model.Id);
            if (imovel is null)
                return (null, "Imóvel não encontrado.", 404);

            imovel.Descricao = model.Descricao;
            imovel.Preco = model.Preco;
            imovel.Estado = model.Estado;
            imovel.EstadoAprovacao = model.EstadoAprovacao;
            imovel.tb04_funcionarioModel = model.tb04_funcionarioModel;
            imovel.tb09_tipo_imovelModel = model.tb09_tipo_imovelModel;
            imovel.tb10_tipologiaModel = model.tb10_tipologiaModel;
            imovel.tb18_proprietarioModel = model.tb18_proprietarioModel;

            return await context.SaveChangesAsync() > 0 ?
                (imovel, "Imóvel actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar imóvel.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



