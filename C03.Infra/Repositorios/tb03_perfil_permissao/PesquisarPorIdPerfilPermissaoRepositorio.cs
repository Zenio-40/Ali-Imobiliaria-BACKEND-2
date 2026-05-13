using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E03_perfil_permissao;

public class PesquisarPorIdPerfilPermissaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb03_perfiL_permissaoModel>
{
    public async Task<(tb03_perfiL_permissaoModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var perfilPermissao = await context.Tabela03PerfilPermissao.FindAsync(id);
            return perfilPermissao is not null ?
                (perfilPermissao, "Perfil Permissão encontrado com sucesso!", 200) :
                (null, "Perfil Permissão não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



