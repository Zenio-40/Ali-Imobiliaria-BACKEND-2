using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E07_telefone;

public class PesquisarPorIdTelefoneRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb07_telefoneModel>
{
    public async Task<(tb07_telefoneModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var telefone = await context.Tabela07Telefone.FindAsync(id);
            return telefone is not null ?
                (telefone, "Telefone encontrado com sucesso!", 200) :
                (null, "Telefone não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



