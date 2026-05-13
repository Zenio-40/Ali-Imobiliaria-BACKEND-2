using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E17_endereco;

public class PesquisarPorIdEnderecoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb17_enderecoModel>
{
    public async Task<(tb17_enderecoModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var entidade = await context.Tabela17Enderco.FindAsync(id);
            return entidade is not null ?
                (entidade, "Endereço encontrado com sucesso!", 200) :
                (null, "Endereço não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



