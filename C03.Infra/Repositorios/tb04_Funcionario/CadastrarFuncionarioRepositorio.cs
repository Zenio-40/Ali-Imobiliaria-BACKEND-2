using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E04_funcionario;

public class CadastrarFuncionarioRepositorio(CorretoraDbContext context) : ICadastrarRepositorio<tb04_funcionarioModel>
{
    public async Task<(tb04_funcionarioModel? dado, string mensagem, int codigo)> CadastrarAsync(tb04_funcionarioModel model)
    {
        try
        {

            
            await context.Tabela04Funcinario.AddAsync(model);
            return await context.SaveChangesAsync() > 0 ?
            (model, "Funcionário cadastrado com sucesso!", 201) : 
            (null, "Erro ao cadastrar funcionário.", 500);
            
        }
        catch (DbUpdateException ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}




