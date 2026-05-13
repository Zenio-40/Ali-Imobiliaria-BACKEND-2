using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ImovelUseCase.Command;

public class CadastrarImovel(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb09_tipo_imovelModel> pesquisarTipo,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb10_tipologiaModel> pesquisarTipologia,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb04_funcionarioModel> pesquisarFuncionario,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb16_bairroModel> pesquisarBairro,
    Corretora.C01.Domain.Interfaces.ICadastrarRepositorio<tb11_imovelModel> cadastrar)
{
    public async Task<(CadastrarImovelDTO? dados, string mensagem, int codigo)> Executar(CadastrarImovelDTO dto)
    {
        var (tipo, _, _) = await pesquisarTipo.PesquisarPorIdAsync(dto.IdTipoImovel);
        if (tipo is null) return (null, "Tipo de imovel nao encontrado", 404);

        var (tipologia, _, _) = await pesquisarTipologia.PesquisarPorIdAsync(dto.IdTipologia);
        if (tipologia is null) return (null, "Tipologia nao encontrada", 404);

        var (funcionario, _, _) = await pesquisarFuncionario.PesquisarPorIdAsync(dto.IdFuncionario);
        if (funcionario is null) return (null, "Funcionario nao encontrado", 404);


        var (bairro, _, _) = await pesquisarBairro.PesquisarPorIdAsync(dto.IdBairro);
        if (bairro is null) return (null, "Bairro nao encontrado", 404);

        var model = new tb11_imovelModel
        {
            Descricao = dto.Descricao,
            Preco = dto.Preco,
            tb09_tipo_imovelModel = dto.IdTipoImovel,
            tb10_tipologiaModel = dto.IdTipologia,
            tb04_funcionarioModel = dto.IdFuncionario,
            Estado = true,
            EstadoAprovacao = "Pendente",
            Foto = dto.Fotos
                .Where(url => !string.IsNullOrWhiteSpace(url))
                .Select(url => new tb12_fotoModel
                {
                    Foto = url.Trim(),
                    tb09_tipo_imovel = dto.IdTipoImovel
                })
                .ToList(),
            Endereco =
            [
                new tb17_enderecoModel
                {
                    tb16_bairroModel = dto.IdBairro,
                    tb09_tipo_imovelModel = dto.IdTipoImovel,
                    Nome = dto.Endereco?.Trim() ?? string.Empty
                }
            ]
        };

        if (!string.IsNullOrWhiteSpace(dto.VideoUrl))
        {
            model.Video =
            [
                new tb13_videoModel
                {
                    Video = dto.VideoUrl.Trim(),
                    tb09_tipo_imovel = dto.IdTipoImovel
                }
            ];
        }

        var (dado, mensagem, codigo) = await cadastrar.CadastrarAsync(model);
        return dado is null ? (null, mensagem, codigo) : (dto, mensagem, codigo);
    }
}
