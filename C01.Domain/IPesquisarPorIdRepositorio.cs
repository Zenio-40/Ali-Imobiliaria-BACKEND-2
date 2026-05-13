using System.Threading.Tasks;

namespace Corretora.C01.Domain;

public interface IPesquisarPorIdRepositorio<T>
{
    Task<(T? dado, string mensagem, int codigo)> PesquisarPorIdAsync(int id, int pagina = 1, int quantidade = 20);
}
