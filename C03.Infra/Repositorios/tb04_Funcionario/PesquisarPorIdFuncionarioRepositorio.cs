using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E04_funcionario;

public class PesquisarPorIdFuncionarioRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb04_funcionarioModel>
{
    public async Task<(tb04_funcionarioModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var funcionario = await context.Tabela04Funcinario.FindAsync(id);
            return funcionario is not null ?
                (funcionario, "Funcionário encontrado com sucesso!", 200) :
                (null, "Funcionário não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}


