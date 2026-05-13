using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Queries;

public class PesquisarFuncionarioPorId(Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb04_funcionarioModel> pesquisar)
{
    public async Task<(FuncionarioDTO? dados, string mensagem, int codigo)> Executar(int id)
    {
        var (funcionario, mensagem, codigo) = await pesquisar.PesquisarPorIdAsync(id);
        if (funcionario is null) return (null, mensagem, codigo);

        var dto = new FuncionarioDTO
        {
            FuncionarioId = funcionario.Id,
            FuncionarioNome = funcionario.Nome,
            FuncionarioTelefone = funcionario.Telefone.FirstOrDefault()?.Numero ?? string.Empty,
            FuncionarioEstado = funcionario.Estado,
            FuncionarioIdPerfil = funcionario.Idtb02_perfilModel,
            FuncionarioPerfil = funcionario.Perfil?.Descricao ?? string.Empty
        };

        return (dto, "Funcionário encontrado com sucesso", 200);
    }
}

