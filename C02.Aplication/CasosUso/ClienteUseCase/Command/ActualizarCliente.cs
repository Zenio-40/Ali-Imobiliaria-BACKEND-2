using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ClienteUseCase.DTOs;

namespace Corretora.C02.Aplication.CasosUso.ClienteUseCase.Command;

public class ActualizarCliente(
    Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb06_clienteModel> pesquisarPorId,
    Corretora.C01.Domain.Interfaces.IActualizarRepositorio<tb06_clienteModel> actualizar)
{
    public async Task<(ActualizarClienteDTO? dados, string mensagem, int codigo)> Executar(ActualizarClienteDTO dto)
    {
        var (cliente, msg, cod) = await pesquisarPorId.PesquisarPorIdAsync(dto.Id);
        if (cliente is null)
            return (null, "Cliente não encontrado", 404);

        cliente.Nome = dto.ClienteNome;
        cliente.Idtb02_perfilModel = dto.ClienteIdPerfil;
        cliente.Estado = dto.Estado;

        if (cliente.Telefone.Any())
            cliente.Telefone.First().Numero = dto.ClienteTelefone;
        else
            cliente.Telefone = new List<tb07_telefoneModel> { new() { Numero = dto.ClienteTelefone } };

        if (cliente.Email.Any())
            cliente.Email.First().Endereco = dto.ClienteEmail;
        else
            cliente.Email = new List<tb08_emailModel> { new() { Endereco = dto.ClienteEmail } };

        var (dado, mensagem, codigo) = await actualizar.ActualizarAsync(cliente);
        return (dto, mensagem, codigo);
    }
}

