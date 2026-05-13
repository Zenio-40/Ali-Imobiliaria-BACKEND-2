using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;
using Corretora.C03.Infra.Data;

namespace Corretora.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin/imoveis")]
public class AdminImovelController(CorretoraDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int pagina = 1, [FromQuery] int quantidade = 20)
    {
        var imoveis = await context.Tabela11Imovel
            .Include(i => i.Funcionario)
            .Include(i => i.TipoImovel)
            .Include(i => i.Tipologia)
            .Include(i => i.ProprietarioModel)
            .Include(i => i.Foto)
            .Include(i => i.Video)
            .Include(i => i.Endereco)
                .ThenInclude(e => e.Bairro)
                    .ThenInclude(b => b.Municipio)
                        .ThenInclude(m => m.Provincia)
            .Skip((pagina - 1) * quantidade)
            .Take(quantidade)
            .ToListAsync();

        var dados = imoveis.Select(ToDto).ToList();
        return Ok(new { dados, mensagem = "Imoveis encontrados", codigo = 200 });
    }

    [HttpPatch("{id}/aprovar")]
    public Task<IActionResult> Aprovar(int id) => AlterarAprovacao(id, "Aprovado");

    [HttpPatch("{id}/reprovar")]
    public Task<IActionResult> Reprovar(int id) => AlterarAprovacao(id, "Reprovado");

    private async Task<IActionResult> AlterarAprovacao(int id, string estado)
    {
        var imovel = await context.Tabela11Imovel.FindAsync(id);
        if (imovel is null)
            return NotFound(new { dados = (object?)null, mensagem = "Imovel nao encontrado", codigo = 404 });

        imovel.EstadoAprovacao = estado;
        imovel.Estado = estado != "Reprovado";
        await context.SaveChangesAsync();

        return Ok(new { dados = new { imovel.Id, imovel.EstadoAprovacao, imovel.Estado }, mensagem = $"Imovel {estado.ToLower()}", codigo = 200 });
    }

    private static ImovelDTO ToDto(tb11_imovelModel i)
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
