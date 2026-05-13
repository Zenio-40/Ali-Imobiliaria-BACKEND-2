using System.Threading.Tasks;

namespace Corretora.C01.Domain;

public interface IAtualizarRepositorio<T>
{
    Task<(T? dado, string mensagem, int codigo)> AtualizarAsync(T model);
}
