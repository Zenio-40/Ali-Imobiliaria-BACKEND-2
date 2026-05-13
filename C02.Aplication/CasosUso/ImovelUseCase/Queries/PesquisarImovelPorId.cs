using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ImovelUseCase.Queries;

public class PesquisarImovelPorId(Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb11_imovelModel> pesquisar)
{
    public async Task<(ImovelDTO? dados, string mensagem, int codigo)> Executar(int id)
    {
        var (imovel, mensagem, codigo) = await pesquisar.PesquisarPorIdAsync(id);
        if (imovel is null) return (null, mensagem, codigo);

        var endereco = imovel.Endereco.FirstOrDefault();
        var dto = new ImovelDTO
        {
            Id = imovel.Id,
            Descricao = imovel.Descricao,
            Preco = imovel.Preco,
            Estado = imovel.Estado,
            EstadoAprovacao = imovel.EstadoAprovacao,
            IdTipoImovel = imovel.tb09_tipo_imovelModel,
            TipoImovel = imovel.TipoImovel?.Descricao ?? string.Empty,
            IdTipologia = imovel.tb10_tipologiaModel,
            Tipologia = imovel.Tipologia?.Descricao ?? string.Empty,
            IdFuncionario = imovel.tb04_funcionarioModel,
            Funcionario = imovel.Funcionario?.Nome ?? string.Empty,
            IdProprietario = imovel.tb18_proprietarioModel,
            Proprietario = imovel.ProprietarioModel?.Nome ?? string.Empty,
            Fotos = imovel.Foto.Select(f => f.Foto).ToList(),
            VideoUrl = imovel.Video.Select(v => v.Video).FirstOrDefault(),
            IdBairro = endereco?.tb16_bairroModel,
            Bairro = endereco?.Bairro?.Nome ?? string.Empty,
            IdMunicipio = endereco?.Bairro?.tb15_municipioModel,
            Municipio = endereco?.Bairro?.Municipio?.Nome ?? string.Empty,
            IdProvincia = endereco?.Bairro?.Municipio?.tb14_provinciaModel,
            Provincia = endereco?.Bairro?.Municipio?.Provincia?.Nome ?? string.Empty,
            Endereco = endereco?.Nome ?? string.Empty
        };

        return (dto, "Imóvel encontrado com sucesso", 200);
    }
}

