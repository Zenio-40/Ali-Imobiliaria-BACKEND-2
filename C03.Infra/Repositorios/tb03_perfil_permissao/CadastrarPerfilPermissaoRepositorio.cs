using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E03_perfil_permissao;

public class CadastrarPerfilPermissaoRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb03_perfiL_permissaoModel>
{
    public async Task<(tb03_perfiL_permissaoModel? dado, string mensagem, int codigo)> CadastrarAsync(tb03_perfiL_permissaoModel model)
    {
        try
        {
            await context.Tabela03PerfilPermissao.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Perfil Permissão cadastrado com sucesso!", 201) :
                (null, "Erro ao cadastrar perfil permissão.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



