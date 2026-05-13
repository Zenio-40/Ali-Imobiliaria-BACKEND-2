using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E05_credencial_acesso;

public class PesquisarPorIdCredencialRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb05_credencial_acessoModel>
{
    public async Task<(tb05_credencial_acessoModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var credencial = await context.Tabela05Credencial.FindAsync(id);
            return credencial is not null ?
                (credencial, "Credencial encontrada com sucesso!", 200) :
                (null, "Credencial não encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



