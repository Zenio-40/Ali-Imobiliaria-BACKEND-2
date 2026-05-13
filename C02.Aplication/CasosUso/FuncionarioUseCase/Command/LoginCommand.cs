using Corretora.C01.Domain.Interfaces;
using Corretora.C02.Aplication.AuthUseCase.DTOs;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;
using Corretora.C02.Aplication.Servico.IPasswordService;
using Corretora.C02.Aplication.Servico.ITokenService;
using Corretora.C03.Infra.Repositorios.E04_funcionario;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Command;

public class LoginCommand(ILoginRepositorio loginRepositorio, IPasswordVerify passwordVerify, ITokenService tokenService)
{
    public async Task<(LoginResponseDTO? dados, string mensagem, int codigo)> Executar(LoginDTO dto)
    {
        var usuario = await loginRepositorio.BuscarPorTelefoneAsync(dto.Telefone);
        if (usuario == null || !usuario.Credencial.Any())
        {
            return (null, "Usuario nao encontrado", 404);
        }

        var credencial = usuario.Credencial.First();
        bool senhaValida = await passwordVerify.VerifyAsync(dto.Senha, credencial.Senha_hash, credencial.Senha_salt);
        if (!senhaValida)
        {
            return (null, "Senha invalida", 401);
        }

        var perfil = usuario.Perfil?.Descricao ?? "Funcionario";
        string token = tokenService.GerarToken(usuario.Id, usuario.Nome ?? "", usuario.Numero ?? "", perfil, "Funcionario");

        return (new LoginResponseDTO
        {
            UsuarioId = usuario.Id,
            Nome = usuario.Nome ?? string.Empty,
            Telefone = usuario.Numero ?? string.Empty,
            Perfil = perfil,
            TipoUsuario = "Funcionario",
            Token = token
        }, "Login realizado com sucesso", 200);
    }
}
