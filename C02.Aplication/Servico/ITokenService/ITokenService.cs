using System;
using Corretora.C01.Domain.Interfaces;

namespace Corretora.C02.Aplication.Servico.ITokenService;

public interface ITokenService
{
string GerarToken(int usuarioId, string nome, string numero, string role, string tipoUsuario); 
}
