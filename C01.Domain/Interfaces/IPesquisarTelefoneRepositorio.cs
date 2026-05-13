using System;

namespace Corretora.C01.Domain.Interfaces;

public interface IPesquisarTelefoneRepositorio<T>
{
 Task<T?> PesquisarAsync(string numero);
}
