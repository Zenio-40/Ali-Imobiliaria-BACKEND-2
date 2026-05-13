using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C02.Aplication.CasosUso.ClienteUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ClienteUseCase.Queries;

public class PesquisarTodosClientes(IPesquisarTodosRepositorio<tb06_clienteModel> pesquisar)
{
    public async Task<(IEnumerable<ClienteDTO>? dados, string mensagem, int codigo)> Executar(int pagina = 1, int quantidade = 20)
    {
        var (clientes, mensagem, codigo) = await pesquisar.PesquisarTodosAsync(pagina, quantidade);
        if (clientes is null)
            return (null, mensagem, codigo);

        var dtoList = clientes.Select(c => new ClienteDTO
        {
            ClienteId = c.Id,
            ClienteNome = c.Nome,
            ClienteTelefone = c.Telefone.FirstOrDefault()?.Numero ?? string.Empty,
            ClienteEmail = c.Email.FirstOrDefault()?.Endereco ?? string.Empty,
            ClienteEstado = c.Estado,
            ClienteIdPerfil = c.Idtb02_perfilModel,
            ClientePerfil = c.Perfil?.Descricao ?? string.Empty
        });

        return (dtoList, "Clientes encontrados com sucesso", 200);
    }
}

