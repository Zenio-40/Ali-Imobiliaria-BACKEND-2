using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Command;

public class ActualizarFuncionario(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb04_funcionarioModel> pesquisar,
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb02_perfilModel> pesquisarPerfil,
    Corretora.C01.Domain.Interfaces.IActualizarRepositorio<tb04_funcionarioModel> actualizar)
{
    public async Task<(ActualizarFuncionarioDTO? dados, string mensagem, int codigo)> Executar(ActualizarFuncionarioDTO dto)
    {
        var (funcionario, _, _) = await pesquisar.PesquisarPorIdAsync(dto.Id);
        if (funcionario is null) return (null, "Funcionário não encontrado", 404);

        var (perfil, _, _) = await pesquisarPerfil.PesquisarPorIdAsync(dto.FuncionarioIdPerfil);
        if (perfil is null) return (null, "Perfil não encontrado", 404);

        funcionario.Nome = dto.FuncionarioNome;
        funcionario.Idtb02_perfilModel = dto.FuncionarioIdPerfil;
        funcionario.Estado = dto.Estado;

        if (funcionario.Telefone.Any())
            funcionario.Telefone.First().Numero = dto.FuncionarioTelefone;
        else
            funcionario.Telefone = new List<tb07_telefoneModel> { new() { Numero = dto.FuncionarioTelefone } };

        var (_, mensagem, codigo) = await actualizar.ActualizarAsync(funcionario);
        return (dto, mensagem, codigo);
    }
}

