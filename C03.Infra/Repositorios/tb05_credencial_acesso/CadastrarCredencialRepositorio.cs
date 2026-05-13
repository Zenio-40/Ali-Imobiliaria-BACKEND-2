using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E05_credencial_acesso;

public class CadastrarCredencialRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb05_credencial_acessoModel>
{
    public async Task<(tb05_credencial_acessoModel? dado, string mensagem, int codigo)> CadastrarAsync(tb05_credencial_acessoModel model)
    {
        try
        {
            await context.Tabela05Credencial.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
                (model, "Credencial cadastrada com sucesso!", 201) :
                (null, "Erro ao cadastrar credencial.", 500);
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



