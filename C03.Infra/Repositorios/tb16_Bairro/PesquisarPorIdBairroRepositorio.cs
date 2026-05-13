using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E16_bairro;

public class PesquisarPorIdBairroRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb16_bairroModel>
{
    public async Task<(tb16_bairroModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var entidade = await context.Tabela16Bairro.FindAsync(id);
            return entidade is not null ?
                (entidade, "Bairro encontrado com sucesso!", 200) :
                (null, "Bairro não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



