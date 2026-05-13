using Corretora.C01.Domain;
using Corretora.C02.Aplication.CasosUso.ClienteUseCase.DTOs;
using Corretora.C02.Aplication.Servico.IPasswordService;

namespace Corretora.C02.Aplication.CasosUso.ClienteUseCase.Command;

public class CadastrarCliente(
    Corretora.C01.Domain.Interfaces.IPesquisarTodosRepositorio<tb06_clienteModel> pesquisarTodos,
    Corretora.C01.Domain.Interfaces.ICadastrarRepositorio<tb06_clienteModel> cadastrar,
    IPasswordHash criptoSenha)
{
    public async Task<(CadastrarClienteDTO? dados, string mensagem, int codigo)> Executar(CadastrarClienteDTO dto)
    {
        var (todos, _, _) = await pesquisarTodos.PesquisarTodosAsync(1, 1000);
        var clienteExistente = todos?.FirstOrDefault(c =>
            c.Telefone.Any(t => t.Numero == dto.ClienteTelefone) ||
            c.Email.Any(e => e.Endereco == dto.ClienteEmail));

        if (clienteExistente is not null)
            return (null, "Cliente com este telefone ou email ja existe.", 409);

        var (senhaHash, senhaSalt) = await criptoSenha.HashAsync(dto.ClienteSenha);

        var model = new tb06_clienteModel
        {
            Nome = dto.ClienteNome,
            Idtb02_perfilModel = dto.ClienteIdPerfil <= 0 ? 3 : dto.ClienteIdPerfil,
            Estado = true,
            Telefone =
            [
                new tb07_telefoneModel
                {
                    Numero = dto.ClienteTelefone
                }
            ],
            Email =
            [
                new tb08_emailModel
                {
                    Endereco = dto.ClienteEmail
                }
            ],
            Credencial =
            [
                new tb05_credencial_acessoModel
                {
                    Senha_hash = senhaHash,
                    Senha_salt = senhaSalt
                }
            ]
        };

        var (dado, mensagem, codigo) = await cadastrar.CadastrarAsync(model);
        return dado is null
            ? (null, mensagem, codigo)
            : (dto, mensagem, codigo);
    }
}
