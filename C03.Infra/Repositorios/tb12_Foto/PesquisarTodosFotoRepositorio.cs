using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Repositorios.E12_foto;

public class PesquisarTodosFotoRepositorio(CorretoraDbContext context) : IPesquisarTodosRepositorio<tb12_fotoModel>
{
    public async Task<(IEnumerable<tb12_fotoModel>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20)
    {
        try
        {
            var dados = await context.Tabela12Foto
                .Skip((pagina - 1) * quantidade)
                .Take(quantidade)
                .ToListAsync();

            return dados.Count > 0 ?
                (dados, "Fotos encontradas com sucesso!", 200) :
                (Array.Empty<tb12_fotoModel>(), "Nenhuma foto encontrada.", 404);
        }
        catch (Exception ex)
        {
            return (null, ex.ToString(), 500);
        }
    }
}



