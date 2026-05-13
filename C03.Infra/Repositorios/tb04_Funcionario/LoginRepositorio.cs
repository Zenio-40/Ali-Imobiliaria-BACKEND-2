using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E04_funcionario;

public interface ILoginRepositorio
{
    Task<tb04_funcionarioModel?> BuscarPorTelefoneAsync(string telefone);
}

public class LoginRepositorio(CorretoraDbContext context) : ILoginRepositorio
{
    public async Task<tb04_funcionarioModel?> BuscarPorTelefoneAsync(string telefone)
    {
        return await context.Tabela04Funcinario
            .Include(f => f.Credencial)
            .Include(f => f.Perfil)
            .Include(f => f.Telefone)
            .FirstOrDefaultAsync(f => f.Numero == telefone || f.Telefone.Any(t => t.Numero == telefone));
    }
}
