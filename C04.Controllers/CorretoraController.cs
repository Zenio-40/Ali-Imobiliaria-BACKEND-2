using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ClienteUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.Command;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;
using Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.Queries;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.Command;
using Corretora.C02.Aplication.CasosUso.SolicitacaoUseCase.DTOs;
using Corretora.C03.Infra.Data;

namespace Corretora.Controllers;

[ApiController]
[Route("api/corretora")]
[Authorize(Roles = "Corretor,Admin")]
public class CorretoraController : ControllerBase
{
    private readonly CorretoraDbContext _context;
    private readonly PesquisarTodosClientes _pesquisarClientes;
    private readonly PesquisarTodosProprietarios _pesquisarProprietarios;
    private readonly CadastrarImovel _cadastrarImovel;
    private readonly ActualizarEstadoSolicitacao _actualizarEstadoSolicitacao;

    public CorretoraController(
        CorretoraDbContext context,
        PesquisarTodosClientes pesquisarClientes,
        PesquisarTodosProprietarios pesquisarProprietarios,
        CadastrarImovel cadastrarImovel,
        ActualizarEstadoSolicitacao actualizarEstadoSolicitacao)
    {
        _context = context;
        _pesquisarClientes = pesquisarClientes;
        _pesquisarProprietarios = pesquisarProprietarios;
        _cadastrarImovel = cadastrarImovel;
        _actualizarEstadoSolicitacao = actualizarEstadoSolicitacao;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard([FromQuery] int? funcionarioId = null)
    {
        var query = _context.Tabela11Imovel.AsQueryable();
        if (funcionarioId.HasValue) query = query.Where(i => i.tb04_funcionarioModel == funcionarioId.Value);

        var totalImoveis = await query.CountAsync();
        var totalPendentes = await query.CountAsync(i => i.EstadoAprovacao == "Pendente");
        var (clientes, _, _) = await _pesquisarClientes.Executar(1, 100);
        var (proprietarios, _, _) = await _pesquisarProprietarios.Executar(1, 100);

        return Ok(new
        {
            mensagem = "Dashboard da Corretora",
            estatisticas = new
            {
                totalImoveis,
                totalPendentes,
                totalClientes = clientes?.Count() ?? 0,
                totalProprietarios = proprietarios?.Count() ?? 0
            }
        });
    }

    [HttpGet("imoveis")]
    public async Task<IActionResult> GetImoveis([FromQuery] int? funcionarioId = null, [FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var query = ImoveisComDetalhes();
        if (funcionarioId.HasValue) query = query.Where(i => i.tb04_funcionarioModel == funcionarioId.Value);

        var imoveis = await query
            .Skip((pagina - 1) * quantidade)
            .Take(quantidade)
            .ToListAsync();
        var dados = imoveis.Select(ToImovelDTO).ToList();

        return Ok(new { dados, mensagem = "Imoveis encontrados", codigo = 200 });
    }

    [HttpPost("imoveis")]
    public async Task<IActionResult> CadastrarImovel([FromBody] CadastrarImovelDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _cadastrarImovel.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("clientes")]
    public async Task<IActionResult> GetClientes([FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var (dados, mensagem, codigo) = await _pesquisarClientes.Executar(pagina, quantidade);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("proprietarios")]
    public async Task<IActionResult> GetProprietarios([FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var (dados, mensagem, codigo) = await _pesquisarProprietarios.Executar(pagina, quantidade);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("solicitacoes")]
    public async Task<IActionResult> GetSolicitacoes([FromQuery] int? funcionarioId = null, [FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var query = _context.Tabela20Solicitacao
            .Include(s => s.Cliente)
            .Include(s => s.Imovel)
            .Include(s => s.EstadoSolicitacao)
            .AsQueryable();

        if (funcionarioId.HasValue)
            query = query.Where(s => s.Imovel.tb04_funcionarioModel == funcionarioId.Value);

        var dados = await query
            .OrderByDescending(s => s.Data)
            .Skip((pagina - 1) * quantidade)
            .Take(quantidade)
            .Select(s => new SolicitacaoDTO
            {
                Id = s.Id,
                IdCliente = s.tb06_clienteModel,
                NomeCliente = s.Cliente.Nome,
                IdImovel = s.tb11_imovelModel,
                DescricaoImovel = s.Imovel.Descricao,
                IdEstadoSolicitacao = s.tb19_estado_solicitacaoModel,
                EstadoSolicitacao = s.EstadoSolicitacao.Nome,
                Data = s.Data
            })
            .ToListAsync();

        return Ok(new { dados, mensagem = "Solicitacoes encontradas", codigo = 200 });
    }

    [HttpPut("solicitacoes/estado")]
    public async Task<IActionResult> AtualizarEstadoSolicitacao([FromBody] ActualizarEstadoSolicitacaoDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (dados, mensagem, codigo) = await _actualizarEstadoSolicitacao.Executar(dto);
        return StatusCode(codigo, new { dados, mensagem, codigo });
    }

    [HttpGet("localizacao/provincias")]
    public async Task<IActionResult> ListarProvincias()
    {
        var dados = await _context.Tabela14Pronvincia.OrderBy(p => p.Nome).ToListAsync();
        return Ok(new { dados, mensagem = "Provincias encontradas", codigo = 200 });
    }

    [HttpPost("localizacao/provincias")]
    public async Task<IActionResult> CadastrarProvincia([FromBody] NomeDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var provincia = new tb14_provinciaModel { Nome = dto.Nome.Trim() };
        _context.Tabela14Pronvincia.Add(provincia);
        await _context.SaveChangesAsync();
        return Created("", new { dados = provincia, mensagem = "Provincia cadastrada", codigo = 201 });
    }

    [HttpGet("localizacao/municipios")]
    public async Task<IActionResult> ListarMunicipios([FromQuery] int? provinciaId = null)
    {
        var query = _context.Tabela15Municipio.Include(m => m.Provincia).AsQueryable();
        if (provinciaId.HasValue) query = query.Where(m => m.tb14_provinciaModel == provinciaId.Value);
        var dados = await query.OrderBy(m => m.Nome).ToListAsync();
        return Ok(new { dados, mensagem = "Municipios encontrados", codigo = 200 });
    }

    [HttpPost("localizacao/municipios")]
    public async Task<IActionResult> CadastrarMunicipio([FromBody] MunicipioDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var exists = await _context.Tabela14Pronvincia.AnyAsync(p => p.Id == dto.IdProvincia);
        if (!exists) return NotFound(new { dados = (object?)null, mensagem = "Provincia nao encontrada", codigo = 404 });

        var municipio = new tb15_municipioModel { Nome = dto.Nome.Trim(), tb14_provinciaModel = dto.IdProvincia };
        _context.Tabela15Municipio.Add(municipio);
        await _context.SaveChangesAsync();
        return Created("", new { dados = municipio, mensagem = "Municipio cadastrado", codigo = 201 });
    }

    [HttpGet("localizacao/bairros")]
    public async Task<IActionResult> ListarBairros([FromQuery] int? municipioId = null)
    {
        var query = _context.Tabela16Bairro.Include(b => b.Municipio).AsQueryable();
        if (municipioId.HasValue) query = query.Where(b => b.tb15_municipioModel == municipioId.Value);
        var dados = await query.OrderBy(b => b.Nome).ToListAsync();
        return Ok(new { dados, mensagem = "Bairros encontrados", codigo = 200 });
    }

    [HttpPost("localizacao/bairros")]
    public async Task<IActionResult> CadastrarBairro([FromBody] BairroDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var exists = await _context.Tabela15Municipio.AnyAsync(m => m.Id == dto.IdMunicipio);
        if (!exists) return NotFound(new { dados = (object?)null, mensagem = "Municipio nao encontrado", codigo = 404 });

        var bairro = new tb16_bairroModel { Nome = dto.Nome.Trim(), tb15_municipioModel = dto.IdMunicipio };
        _context.Tabela16Bairro.Add(bairro);
        await _context.SaveChangesAsync();
        return Created("", new { dados = bairro, mensagem = "Bairro cadastrado", codigo = 201 });
    }

    private IQueryable<tb11_imovelModel> ImoveisComDetalhes()
    {
        return _context.Tabela11Imovel
            .Include(i => i.Funcionario)
            .Include(i => i.TipoImovel)
            .Include(i => i.Tipologia)
            .Include(i => i.ProprietarioModel)
            .Include(i => i.Foto)
            .Include(i => i.Video)
            .Include(i => i.Endereco)
                .ThenInclude(e => e.Bairro)
                    .ThenInclude(b => b.Municipio)
                        .ThenInclude(m => m.Provincia);
    }

    private static ImovelDTO ToImovelDTO(tb11_imovelModel i)
    {
        var endereco = i.Endereco.FirstOrDefault();
        return new ImovelDTO
        {
            Id = i.Id,
            Descricao = i.Descricao,
            Preco = i.Preco,
            Estado = i.Estado,
            EstadoAprovacao = i.EstadoAprovacao,
            IdTipoImovel = i.tb09_tipo_imovelModel,
            TipoImovel = i.TipoImovel?.Descricao ?? string.Empty,
            IdTipologia = i.tb10_tipologiaModel,
            Tipologia = i.Tipologia?.Descricao ?? string.Empty,
            IdFuncionario = i.tb04_funcionarioModel,
            Funcionario = i.Funcionario?.Nome ?? string.Empty,
            IdProprietario = i.tb18_proprietarioModel,
            Proprietario = i.ProprietarioModel?.Nome ?? string.Empty,
            Fotos = i.Foto.Select(f => f.Foto).ToList(),
            VideoUrl = i.Video.Select(v => v.Video).FirstOrDefault(),
            IdBairro = endereco?.tb16_bairroModel,
            Bairro = endereco?.Bairro?.Nome ?? string.Empty,
            IdMunicipio = endereco?.Bairro?.tb15_municipioModel,
            Municipio = endereco?.Bairro?.Municipio?.Nome ?? string.Empty,
            IdProvincia = endereco?.Bairro?.Municipio?.tb14_provinciaModel,
            Provincia = endereco?.Bairro?.Municipio?.Provincia?.Nome ?? string.Empty,
            Endereco = endereco?.Nome ?? string.Empty
        };
    }
}

public class NomeDTO
{
    [Required]
    public string Nome { get; set; } = string.Empty;
}

public class MunicipioDTO : NomeDTO
{
    [Required]
    public int IdProvincia { get; set; }
}

public class BairroDTO : NomeDTO
{
    [Required]
    public int IdMunicipio { get; set; }
}
