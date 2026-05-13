using System;

namespace Corretora.C01.Domain.Interfaces;

public interface IRemoverRepositorio<T>
{
    Task<(bool sucesso, string mensagem, int codigo)> RemoverAsync(int id);
}
