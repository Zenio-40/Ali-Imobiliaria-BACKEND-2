using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C02.Aplication.CasosUso.PerfilUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.PerfilUseCase.Command;

public class CadastrarPerfil(ICadastrarRepositorio<tb02_perfilModel> cadastrar)
{
    public async Task<(CadastrarPerfilDTO? dados, string mensagem, int codigo)> Executar(CadastrarPerfilDTO dto)
    {
        var model = new tb02_perfilModel
        {
            Descricao = dto.Descricao
        };

        var (dado, mensagem, codigo) = await cadastrar.CadastrarAsync(model);
        return dado is null
            ? (null, mensagem, codigo)
            : (dto, mensagem, codigo);
    }
}

