using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Queries;

public class PesquisarTodosFuncionarios(Corretora.C01.Domain.Interfaces.IPesquisarTodosRepositorio<tb04_funcionarioModel> pesquisar)
{
    public async Task<(IEnumerable<FuncionarioDTO>? dados, string mensagem, int codigo)> Executar(int pagina = 1, int quantidade = 20)
    {
        var (funcionarios, mensagem, codigo) = await pesquisar.PesquisarTodosAsync(pagina, quantidade);
        if (funcionarios is null) return (null, mensagem, codigo);

        var dtoList = funcionarios.Select(f => new FuncionarioDTO
        {
            FuncionarioId = f.Id,
            FuncionarioNome = f.Nome,
            FuncionarioTelefone = f.Numero,
            FuncionarioEstado = f.Estado,
            FuncionarioIdPerfil = f.Idtb02_perfilModel,
            FuncionarioPerfil = f.Perfil?.Descricao ?? string.Empty
        });

        return (dtoList, "Funcionários encontrados com sucesso", 200);
    }
}

