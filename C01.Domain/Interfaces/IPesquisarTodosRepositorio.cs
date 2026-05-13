using System;
using System.Collections.Generic;

namespace Corretora.C01.Domain.Interfaces;

public interface IPesquisarTodosRepositorio<T>
{
    Task<(IEnumerable<T>? dados, string mensagem, int codigo)> PesquisarTodosAsync(int pagina = 1, int quantidade = 20);
}
