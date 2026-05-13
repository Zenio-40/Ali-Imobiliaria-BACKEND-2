using System.Globalization;
using System.Linq;
using System.Text;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C02.Aplication.AuthUseCase.DTOs;
using Corretora.C02.Aplication.Servico.IPasswordService;
using Corretora.C02.Aplication.Servico.ITokenService;

namespace Corretora.C02.Aplication.AuthUseCase.Command;

public class LoginUsuario(
    Corretora.C01.Domain.Interfaces.IPesquisarTelefoneRepositorio<tb04_funcionarioModel> pesquisarFuncionario,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb02_perfilModel> pesquisarPerfil,
    IPasswordVerify passwordVerify,
    ITokenService tokenService)
{
    public async Task<(LoginResponseDTO? dados, string mensagem, int codigo)> Executar(LoginRequestDTO dto)
    {
        var funcionario = await pesquisarFuncionario.PesquisarAsync(dto.Telefone);
        if (funcionario is null)
            return (null, "Credenciais invalidas.", 401);

        if (!funcionario.Estado)
            return (null, "Usuario desativado.", 403);

        var credencial = funcionario.Credencial.FirstOrDefault();
        if (credencial == null)
            return (null, "Credenciais invalidas.", 401);

        bool senhaOk = await passwordVerify.VerifyAsync(dto.Senha, credencial.Senha_hash, credencial.Senha_salt);
        if (!senhaOk)
            return (null, "Credenciais invalidas.", 401);

        var resultado = await pesquisarPerfil.PesquisarPorIdAsync(funcionario.Idtb02_perfilModel);
        string role = NormalizarPerfil(resultado.dado?.Descricao);

        string telefone = funcionario.Telefone.FirstOrDefault()?.Numero ?? "";

        string token = tokenService.GerarToken(funcionario.Id, funcionario.Nome, telefone, role, "Funcionario");
        var response = new LoginResponseDTO
        {
            UsuarioId = funcionario.Id,
            Nome = funcionario.Nome,
            Telefone = telefone,
            Perfil = role,
            TipoUsuario = "Funcionario",
            Token = token
        };

        return (response, "Login realizado com sucesso.", 200);
    }

    private static string NormalizarPerfil(string? descricaoPerfil)
    {
        if (string.IsNullOrWhiteSpace(descricaoPerfil))
            return "Funcionario";

        string semAcentos = RemoverAcentos(descricaoPerfil).Trim().ToLowerInvariant();

        if (semAcentos.Contains("admin"))
            return "Admin";

        if (semAcentos.Contains("sind"))
            return "Sindico";

        if (semAcentos.Contains("inquilino"))
            return "Inquilino";

        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(semAcentos);
    }

    private static string RemoverAcentos(string texto)
    {
        string normalized = texto.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();

        foreach (char c in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                builder.Append(c);
        }

        return builder.ToString().Normalize(NormalizationForm.FormC);
    }
}
