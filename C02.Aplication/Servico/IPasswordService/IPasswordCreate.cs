using System;

namespace Corretora.C02.Aplication.Servico.IPasswordService;

public interface IPasswordCreate
{
    Task<string> GenerateAsync();
}
