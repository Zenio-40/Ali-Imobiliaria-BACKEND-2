using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Corretora.C02.Aplication.CasosUso.ClienteUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Command;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.Command;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.PerfilUseCase.Command;
using Corretora.C02.Aplication.CasosUso.PerfilUseCase.DTOs;
using Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.Command;
using Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.DTOs;
using Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Command;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Queries;

namespace Corretora.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly CadastrarFuncionario _cadastrarFuncionario;
    private readonly ActualizarFuncionario _actualizarFuncionario;
    private readonly DesativarFuncionario _desativarFuncionario;
    private readonly PesquisarFuncionarioPorId _pesquisarFuncionarioPorId;
    private readonly PesquisarTodosFuncionarios _pesquisarTodosFuncionarios;

    private readonly CadastrarProprietario _cadastrarProprietario;
    private readonly ActualizarProprietario _actualizarProprietario;
    private readonly PesquisarProprietarioPorId _pesquisarProprietarioPorId;
    private readonly PesquisarTodosProprietarios _pesquisarTodosProprietarios;

    private readonly CadastrarImovel _cadastrarImovel;
    private readonly ActualizarImovel _actualizarImovel;
    private readonly DesativarImovel _desativarImovel;
    private readonly PesquisarImovelPorId _pesquisarImovelPorId;
    private readonly PesquisarImoveisDisponiveis _pesquisarImoveisDisponiveis;

    private readonly CadastrarPerfil _cadastrarPerfil;

    private readonly CadastrarSolicitacao _cadastrarSolicitacao;
    private readonly ActualizarEstadoSolicitacao _actualizarEstadoSolicitacao;
    private readonly PesquisarSolicitacaoPorId _pesquisarSolicitacaoPorId;
    private readonly PesquisarSolicitacoesDoCliente _pesquisarSolicitacoesDoCliente;

    private readonly PesquisarTodosClientes _pesquisarTodosClientes;

    public AdminController(
        CadastrarFuncionario cadastrarFuncionario,
        ActualizarFuncionario actualizarFuncionario,
        DesativarFuncionario desativarFuncionario,
        PesquisarFuncionarioPorId pesquisarFuncionarioPorId,
        PesquisarTodosFuncionarios pesquisarTodosFuncionarios,
        CadastrarProprietario cadastrarProprietario,
        ActualizarProprietario actualizarProprietario,
        PesquisarProprietarioPorId pesquisarProprietarioPorId,
        PesquisarTodosProprietarios pesquisarTodosProprietarios,
        CadastrarImovel cadastrarImovel,
        ActualizarImovel actualizarImovel,
        DesativarImovel desativarImovel,
        PesquisarImovelPorId pesquisarImovelPorId,
        PesquisarImoveisDisponiveis pesquisarImoveisDisponiveis,
        CadastrarPerfil cadastrarPerfil,
        CadastrarSolicitacao cadastrarSolicitacao,
        ActualizarEstadoSolicitacao actualizarEstadoSolicitacao,
        PesquisarSolicitacaoPorId pesquisarSolicitacaoPorId,
        PesquisarSolicitacoesDoCliente pesquisarSolicitacoesDoCliente,
        PesquisarTodosClientes pesquisarTodosClientes)
    {
        _cadastrarFuncionario = cadastrarFuncionario;
        _actualizarFuncionario = actualizarFuncionario;
        _desativarFuncionario = desativarFuncionario;
        _pesquisarFuncionarioPorId = pesquisarFuncionarioPorId;
        _pesquisarTodosFuncionarios = pesquisarTodosFuncionarios;

        _cadastrarProprietario = cadastrarProprietario;
        _actualizarProprietario = actualizarProprietario;
        _pesquisarProprietarioPorId = pesquisarProprietarioPorId;
        _pesquisarTodosProprietarios = pesquisarTodosProprietarios;

        _cadastrarImovel = cadastrarImovel;
        _actualizarImovel = actualizarImovel;
        _desativarImovel = desativarImovel;
        _pesquisarImovelPorId = pesquisarImovelPorId;
        _pesquisarImoveisDisponiveis = pesquisarImoveisDisponiveis;

        _cadastrarPerfil = cadastrarPerfil;

        _cadastrarSolicitacao = cadastrarSolicitacao;
        _actualizarEstadoSolicitacao = actualizarEstadoSolicitacao;
        _pesquisarSolicitacaoPorId = pesquisarSolicitacaoPorId;
        _pesquisarSolicitacoesDoCliente = pesquisarSolicitacoesDoCliente;

        _pesquisarTodosClientes = pesquisarTodosClientes;
    }

    [HttpPost("funcionarios")]
    public async Task<IActionResult> CadastrarFuncionario([FromBody] CadastrarFuncionarioDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _cadastrarFuncionario.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("funcionarios")]
    public async Task<IActionResult> ListarFuncionarios([FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var (dados, mensagem, codigo) = await _pesquisarTodosFuncionarios.Executar(pagina, quantidade);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("funcionarios/{id}")]
    public async Task<IActionResult> ObterFuncionario(int id)
    {
        var (dados, mensagem, codigo) = await _pesquisarFuncionarioPorId.Executar(id);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpPut("funcionarios")]
    public async Task<IActionResult> AtualizarFuncionario([FromBody] ActualizarFuncionarioDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _actualizarFuncionario.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpPatch("funcionarios/{id}/desativar")]
    public async Task<IActionResult> DesativarFuncionario(int id)
    {
        var (sucesso, mensagem, codigo) = await _desativarFuncionario.Executar(id);
        return StatusCode(codigo, new { sucesso, mensagem, codigo });
    }



    [HttpPost("proprietarios")]
    public async Task<IActionResult> CadastrarProprietario([FromBody] CadastrarProprietarioDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _cadastrarProprietario.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpPut("proprietarios")]
    public async Task<IActionResult> AtualizarProprietario([FromBody] ActualizarProprietarioDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _actualizarProprietario.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("proprietarios")]
    public async Task<IActionResult> ListarProprietarios([FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var (dados, mensagem, codigo) = await _pesquisarTodosProprietarios.Executar(pagina, quantidade);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("proprietarios/{id}")]
    public async Task<IActionResult> ObterProprietario(int id)
    {
        var (dados, mensagem, codigo) = await _pesquisarProprietarioPorId.Executar(id);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpPost("imoveis")]
    public async Task<IActionResult> CadastrarImovel([FromBody] CadastrarImovelDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _cadastrarImovel.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpPut("imoveis")]
    public async Task<IActionResult> AtualizarImovel([FromBody] ActualizarImovelDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _actualizarImovel.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpPatch("imoveis/{id}/desativar")]
    public async Task<IActionResult> DesativarImovel(int id)
    {
        var (sucesso, mensagem, codigo) = await _desativarImovel.Executar(id);
        return StatusCode(codigo, new { sucesso, mensagem, codigo });
    }

    [HttpGet("imoveis/{id}")]
    public async Task<IActionResult> ObterImovel(int id)
    {
        var (dados, mensagem, codigo) = await _pesquisarImovelPorId.Executar(id);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("imoveis/disponiveis")]
    public async Task<IActionResult> ListarImoveisDisponiveis([FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var (dados, mensagem, codigo) = await _pesquisarImoveisDisponiveis.Executar(pagina, quantidade);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpPost("perfis")]
    public async Task<IActionResult> CadastrarPerfil([FromBody] CadastrarPerfilDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _cadastrarPerfil.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpPut("solicitacoes/estado")]
    public async Task<IActionResult> AtualizarEstadoSolicitacao([FromBody] ActualizarEstadoSolicitacaoDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _actualizarEstadoSolicitacao.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("solicitacoes/{id}")]
    public async Task<IActionResult> ObterSolicitacao(int id)
    {
        var (dados, mensagem, codigo) = await _pesquisarSolicitacaoPorId.Executar(id);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("solicitacoes/cliente/{clienteId}")]
    public async Task<IActionResult> ListarSolicitacoesPorCliente(int clienteId, [FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var (dados, mensagem, codigo) = await _pesquisarSolicitacoesDoCliente.Executar(clienteId, pagina, quantidade);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("clientes")]
    public async Task<IActionResult> ListarClientes([FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var (dados, mensagem, codigo) = await _pesquisarTodosClientes.Executar(pagina, quantidade);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var (imoveis, _, _) = await _pesquisarImoveisDisponiveis.Executar(1, 100);
        var (clientes, _, _) = await _pesquisarTodosClientes.Executar(1, 100);
        var (proprietarios, _, _) = await _pesquisarTodosProprietarios.Executar(1, 100);

        return Ok(new
        {
            mensagem = "Dashboard de administração",
            estatisticas = new
            {
                totalImoveis = imoveis?.Count() ?? 0,
                totalClientes = clientes?.Count() ?? 0,
                totalProprietarios = proprietarios?.Count() ?? 0
            }
        });
    }
}
