using System;

namespace Corretora.C02.Aplication.Servico.IPasswordService;

public interface IPasswordHash
{
    Task<(string hash, string salt)> HashAsync(string senha);
}
