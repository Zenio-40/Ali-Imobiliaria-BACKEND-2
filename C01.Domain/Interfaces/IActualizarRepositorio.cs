using System;

namespace Corretora.C01.Domain.Interfaces;

public interface IActualizarRepositorio<T>
{
    Task<(T? dado, string mensagem, int codigo)> ActualizarAsync(T model);
}
