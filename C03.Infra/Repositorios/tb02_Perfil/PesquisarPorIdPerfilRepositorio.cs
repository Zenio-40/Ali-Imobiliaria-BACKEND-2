using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E02_perfil;

public class PesquisarPorIdPerfilRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb02_perfilModel>
{
    public async Task<(tb02_perfilModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var perfil = await context.Tabela02Perfil.FindAsync(id);
            return perfil is not null ?
                (perfil, "Perfil encontrado com sucesso!", 200) :
                (null, "Perfil não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



