using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Corretora.C02.Aplication.AuthUseCase.DTOs;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Command;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;
using Corretora.C02.Aplication.Servico.IPasswordService;
using Corretora.C02.Aplication.Servico.ITokenService;
using Corretora.C03.Infra.Data;

namespace Corretora.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly LoginCommand _loginFuncionario;
    private readonly CorretoraDbContext _context;
    private readonly IPasswordVerify _passwordVerify;
    private readonly IPasswordHash _passwordHash;
    private readonly ITokenService _tokenService;

    public AuthController(
        LoginCommand loginFuncionario,
        CorretoraDbContext context,
        IPasswordVerify passwordVerify,
        IPasswordHash passwordHash,
        ITokenService tokenService)
    {
        _loginFuncionario = loginFuncionario;
        _context = context;
        _passwordVerify = passwordVerify;
        _passwordHash = passwordHash;
        _tokenService = tokenService;
    }

    [HttpPost("login/funcionario")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginFuncionario([FromBody] LoginDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (dados, mensagem, codigo) = await _loginFuncionario.Executar(dto);
        return StatusCode(codigo, new { dados, token = dados?.Token, mensagem, codigo });
    }

    [HttpPost("login/cliente")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginCliente([FromBody] LoginClienteDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var cliente = await BuscarClientePorEmailOuTelefone(dto.EmailOuTelefone);
        if (cliente is null || !cliente.Credencial.Any())
            return NotFound(new { dados = (object?)null, mensagem = "Cliente nao encontrado", codigo = 404 });

        var credencial = cliente.Credencial.First();
        var senhaValida = await _passwordVerify.VerifyAsync(dto.Senha, credencial.Senha_hash, credencial.Senha_salt);
        if (!senhaValida)
            return Unauthorized(new { dados = (object?)null, mensagem = "Senha invalida", codigo = 401 });

        var telefone = cliente.Telefone.Select(t => t.Numero).FirstOrDefault() ?? string.Empty;
        var email = cliente.Email.Select(e => e.Endereco).FirstOrDefault() ?? string.Empty;
        var token = _tokenService.GerarToken(cliente.Id, cliente.Nome, telefone, "Cliente", "Cliente");

        var dados = new LoginResponseDTO
        {
            UsuarioId = cliente.Id,
            Nome = cliente.Nome,
            Telefone = telefone,
            Email = email,
            Perfil = "Cliente",
            TipoUsuario = "Cliente",
            Token = token
        };

        return Ok(new { dados, token, mensagem = "Login realizado com sucesso", codigo = 200 });
    }

    [HttpPut("cliente/repor-senha")]
    public async Task<IActionResult> ReporSenhaCliente([FromBody] ReporSenhaClienteDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var cliente = await BuscarClientePorEmailOuTelefone(dto.EmailOuTelefone);
        if (cliente is null || !cliente.Credencial.Any())
            return NotFound(new { dados = (object?)null, mensagem = "Cliente nao encontrado", codigo = 404 });

        var credencial = cliente.Credencial.First();
        var senhaAtualValida = await _passwordVerify.VerifyAsync(dto.SenhaAtual, credencial.Senha_hash, credencial.Senha_salt);
        if (!senhaAtualValida)
            return Unauthorized(new { dados = (object?)null, mensagem = "Senha atual invalida", codigo = 401 });

        var (hash, salt) = await _passwordHash.HashAsync(dto.NovaSenha);
        credencial.Senha_hash = hash;
        credencial.Senha_salt = salt;

        await _context.SaveChangesAsync();
        return Ok(new { dados = true, mensagem = "Senha reposta com sucesso", codigo = 200 });
    }

    private Task<Corretora.C01.Domain.tb06_clienteModel?> BuscarClientePorEmailOuTelefone(string emailOuTelefone)
    {
        return _context.Tabela06Cliente
            .Include(c => c.Perfil)
            .Include(c => c.Telefone)
            .Include(c => c.Email)
            .Include(c => c.Credencial)
            .FirstOrDefaultAsync(c =>
                c.Email.Any(e => e.Endereco == emailOuTelefone) ||
                c.Telefone.Any(t => t.Numero == emailOuTelefone));
    }
}

public class LoginClienteDTO
{
    [Required]
    public string EmailOuTelefone { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}

public class ReporSenhaClienteDTO
{
    [Required]
    public string EmailOuTelefone { get; set; } = string.Empty;

    [Required]
    public string SenhaAtual { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string NovaSenha { get; set; } = string.Empty;
}
