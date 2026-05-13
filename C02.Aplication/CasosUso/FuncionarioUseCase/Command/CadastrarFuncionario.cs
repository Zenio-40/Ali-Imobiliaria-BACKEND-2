using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;
using Corretora.C02.Aplication.Servico.IPasswordService;
using Corretora.C02.Aplication.Servico.ISmsService;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.Command;

public class CadastrarFuncionario(
    ICadastrarRepositorio<tb04_funcionarioModel> cadastrar,
    IPasswordCreate gerarSenha,
    IPasswordHash criptoSenha,
    ISmsService smsService,
    IPesquisarTelefoneRepositorio<tb04_funcionarioModel> pesquisar)
{
    public async Task<(object? dados, string mensagem, int codigo)> Executar(CadastrarFuncionarioDTO dto)
    {
        var usuarioExistente = await pesquisar.PesquisarAsync(dto.FuncionarioTelefone);
        if (usuarioExistente is not null)
            return (null, "Funcionário já cadastrado", 409);

        var senha = await gerarSenha.GenerateAsync();
        var (senhaHash, senhaSalt) = await criptoSenha.HashAsync(senha);

        var model = new tb04_funcionarioModel
        {
            Nome = dto.FuncionarioNome,
            Numero = dto.FuncionarioTelefone,
            Idtb02_perfilModel = dto.FuncionarioIdPerfil,
            Estado = true,
            Telefone = new List<tb07_telefoneModel>
            {
                new()
                {
                    Numero = dto.FuncionarioTelefone
                }
            },
            Credencial = new List<tb05_credencial_acessoModel>
            {
                new()
                {
                    Senha_hash = senhaHash,
                    Senha_salt = senhaSalt
                }
            }
        };

        var (dado, mensagem, codigo) = await cadastrar.CadastrarAsync(model);
        if (dado is null)
            return (null, mensagem, codigo);

        var texto = $"Olá sr(a) {model.Nome}, foste cadastrado na plataforma da Ali-Imobiliária. Acesse: www.Aliimobiliaria.com. Credenciais - Telefone: {model.Numero} e Senha: {senha}";

        // no seu SmsService o 3º parâmetro chama-se nif; aqui mantemos o valor fixo que o projecto já usa.
        _ = await smsService.EnviarSmsAsync(model.Numero, texto, "101010101010");

        return (dado, mensagem, codigo);
    }
}


