using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ClienteUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ClienteUseCase.Queries;

public class PesquisarClientePorId(Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb06_clienteModel> pesquisar)
{
    public async Task<(ClienteDTO? dados, string mensagem, int codigo)> Executar(int id)
    {
        var (cliente, mensagem, codigo) = await pesquisar.PesquisarPorIdAsync(id);
        if (cliente is null)
            return (null, mensagem, codigo);

        var dto = new ClienteDTO
        {
            ClienteId = cliente.Id,
            ClienteNome = cliente.Nome,
            ClienteTelefone = cliente.Telefone.FirstOrDefault()?.Numero ?? string.Empty,
            ClienteEmail = cliente.Email.FirstOrDefault()?.Endereco ?? string.Empty,
            ClienteEstado = cliente.Estado,
            ClienteIdPerfil = cliente.Idtb02_perfilModel,
            ClientePerfil = cliente.Perfil?.Descricao ?? string.Empty
        };

        return (dto, "Cliente encontrado com sucesso", 200);
    }
}
