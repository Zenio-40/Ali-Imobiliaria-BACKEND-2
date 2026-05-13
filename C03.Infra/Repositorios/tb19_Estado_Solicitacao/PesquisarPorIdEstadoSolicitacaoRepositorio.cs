using System;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E19_estado_solicitacao;

public class PesquisarPorIdEstadoSolicitacaoRepositorio(CorretoraDbContext context) : Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb19_estado_solicitacaoModel>
{
    public async Task<(tb19_estado_solicitacaoModel? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20)
    {
        try
        {
            var entidade = await context.Tabela19EstadoSolicitacao.FindAsync(id);
            return entidade is not null ?
                (entidade, "Estado de solicitação encontrado com sucesso!", 200) :
                (null, "Estado de solicitação não encontrado.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



