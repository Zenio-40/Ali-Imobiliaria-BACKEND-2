using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corretora.C01.Domain;

public interface IPesquisarPorTextoRepositorio<T>
{
    Task<(IEnumerable<T> dados, string mensagem, int codigo)> PesquisarPorTextoAsync(string texto, int pagina = 1, int quantidade = 20);
}
