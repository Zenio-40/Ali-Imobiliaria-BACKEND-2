using Corretora.C01.Domain;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Command;

public class DesativarFuncionario(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb04_funcionarioModel> pesquisar,
    Corretora.C01.Domain.Interfaces.IActualizarRepositorio<tb04_funcionarioModel> actualizar)
{
    public async Task<(bool sucesso, string mensagem, int codigo)> Executar(int id)
    {
        var (funcionario, _, _) = await pesquisar.PesquisarPorIdAsync(id);
        if (funcionario is null) return (false, "Funcionário não encontrado", 404);

        funcionario.Estado = false;
        var (_, mensagem, codigo) = await actualizar.ActualizarAsync(funcionario);
        return (codigo == 200, mensagem, codigo);
    }
}

