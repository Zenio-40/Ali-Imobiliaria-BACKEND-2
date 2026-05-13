using Corretora.C01.Domain;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Command;

public class RemoverFuncionario(Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb04_funcionarioModel> remover)
{
    public async Task<(bool sucesso, string mensagem, int codigo)> Executar(int id)
    {
        return await remover.RemoverAsync(id);
    }
}


