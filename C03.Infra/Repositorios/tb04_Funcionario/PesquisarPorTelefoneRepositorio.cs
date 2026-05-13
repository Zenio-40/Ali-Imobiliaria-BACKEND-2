using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E04_funcionario;

public class PesquisarPorTelefoneRepositorio(CorretoraDbContext context) : IPesquisarTelefoneRepositorio<tb04_funcionarioModel>
{
    public async Task<tb04_funcionarioModel?> PesquisarAsync(string numero)
    {
        return await context.Tabela04Funcinario
            .Include(f => f.Telefone)
            .FirstOrDefaultAsync(x => x.Numero == numero || x.Telefone.Any(t => t.Numero == numero));
    }
}



