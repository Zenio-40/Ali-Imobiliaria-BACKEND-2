using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E15_municipio;

public class ActualizarMunicipioRepositorio(CorretoraDbContext context) : IActualizarRepositorio<tb15_municipioModel>
{
    public async Task<(tb15_municipioModel? dado, string mensagem, int codigo)> ActualizarAsync(tb15_municipioModel model)
    {
        try
        {
            var municipio = await context.Tabela15Municipio.FindAsync(model.Id);
            if (municipio is null)
                return (null, "Município não encontrado.", 404);

            municipio.Nome = model.Nome;
            municipio.tb14_provinciaModel = model.tb14_provinciaModel;

            return await context.SaveChangesAsync() > 0 ?
                (municipio, "Município actualizado com sucesso!", 200) :
                (null, "Erro ao actualizar município.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



