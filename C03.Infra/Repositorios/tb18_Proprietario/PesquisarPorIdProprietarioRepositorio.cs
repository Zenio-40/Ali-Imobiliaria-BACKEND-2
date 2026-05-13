using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E18_proprietario;

public class PesquisarPorIdProprietarioRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb18_proprietarioModel>
{
    public async Task<(tb18_proprietarioModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var entidade = await context.Tabela18Proprietario.FindAsync(id);
            return entidade is not null ?
                (entidade, "Proprietário encontrado com sucesso!", 200) :
                (null, "Proprietário não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



