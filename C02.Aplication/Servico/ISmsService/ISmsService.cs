using System;

namespace Corretora.C02.Aplication.Servico.ISmsService;

public interface ISmsService
{
Task<bool> EnviarSmsAsync(string numero, string mensagem, string nif);
}
