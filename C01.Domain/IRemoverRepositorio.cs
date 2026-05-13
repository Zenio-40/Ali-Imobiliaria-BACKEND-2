using System.Threading.Tasks;

namespace Corretora.C01.Domain;

public interface IRemoverRepositorio<T>
{
    Task<(T? dado, string mensagem, int codigo)> RemoverAsync(T model);
}
