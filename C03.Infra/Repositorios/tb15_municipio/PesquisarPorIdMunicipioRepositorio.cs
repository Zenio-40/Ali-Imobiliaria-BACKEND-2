using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E15_municipio;

public class PesquisarPorIdMunicipioRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb15_municipioModel>
{
    public async Task<(tb15_municipioModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var municipio = await context.Tabela15Municipio.FindAsync(id);
            return municipio is not null ?
                (municipio, "Município encontrado com sucesso!", 200) :
                (null, "Município não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



