using System;

namespace Corretora.C02.Aplication.Servico.IPasswordService;

public interface IPasswordVerify
{
    Task<bool> VerifyAsync(string senha, string hashArmazenado, string saltArmazenado);
}
