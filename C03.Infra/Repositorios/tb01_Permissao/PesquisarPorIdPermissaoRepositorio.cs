using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E01_permissao;

public class PesquisarPorIdPermissaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb01_permissaoModel>
{
    public async Task<(tb01_permissaoModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var permissao = await context.Tabela01Permissao.FindAsync(id);
            return permissao is not null ?
                (permissao, "Permissão encontrada com sucesso!", 200) :
                (null, "Permissão não encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}


