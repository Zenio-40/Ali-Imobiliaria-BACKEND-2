using Corretora.C01.Domain;
using Corretora.C01.Domain.Interfaces;
using Corretora.C03.Infra.Repositorios.E01_permissao;
using Corretora.C03.Infra.Repositorios.E02_perfil;
using Corretora.C03.Infra.Repositorios.E03_perfil_permissao;
using Corretora.C03.Infra.Repositorios.E04_funcionario;
using Corretora.C03.Infra.Repositorios.E05_credencial_acesso;
using Corretora.C03.Infra.Repositorios.E06_cliente;
using Corretora.C03.Infra.Repositorios.E07_telefone;
using Corretora.C03.Infra.Repositorios.E08_email;
using Corretora.C03.Infra.Repositorios.E09_tipo_imovel;
using Corretora.C03.Infra.Repositorios.E10_tipologia;
using Corretora.C03.Infra.Repositorios.E11_imovel;
using Corretora.C03.Infra.Repositorios.E12_foto;
using Corretora.C03.Infra.Repositorios.E13_video;
using Corretora.C03.Infra.Repositorios.E14_provincia;
using Corretora.C03.Infra.Repositorios.E15_municipio;
using Corretora.C03.Infra.Repositorios.E16_bairro;
using Corretora.C03.Infra.Repositorios.E17_endereco;
using Corretora.C03.Infra.Repositorios.E18_proprietario;
using Corretora.C03.Infra.Repositorios.E19_estado_solicitacao;
using Corretora.C03.Infra.Repositorios.E20_solicitacao;
using Corretora.C03.Infra.Repositorios.E21_favorito;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Corretora;

public static class Contratos
{
    public static IServiceCollection AddContratos(this IServiceCollection services)
    {
        // Permissão
        services.AddScoped<ICadastrarRepositorio<tb01_permissaoModel>, CadastrarPermissaoRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb01_permissaoModel>, ActualizarPermissaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb01_permissaoModel>, PesquisarPorIdPermissaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb01_permissaoModel>, PesquisarPorTextoPermissaoRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb01_permissaoModel>, PesquisarTodosPermissaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb01_permissaoModel>, RemoverPermissaoRepositorio>();

        // Perfil
        services.AddScoped<ICadastrarRepositorio<tb02_perfilModel>, CadastrarPerfilRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb02_perfilModel>, ActualizarPerfilRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb02_perfilModel>, PesquisarPorIdPerfilRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb02_perfilModel>, PesquisarPorTextoPerfilRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb02_perfilModel>, PesquisarTodosPerfilRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb02_perfilModel>, RemoverPerfilRepositorio>();

        // PerfilPermissão
        services.AddScoped<ICadastrarRepositorio<tb03_perfiL_permissaoModel>, CadastrarPerfilPermissaoRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb03_perfiL_permissaoModel>, ActualizarPerfilPermissaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb03_perfiL_permissaoModel>, PesquisarPorIdPerfilPermissaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb03_perfiL_permissaoModel>, PesquisarPorTextoPerfilPermissaoRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb03_perfiL_permissaoModel>, PesquisarTodosPerfilPermissaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb03_perfiL_permissaoModel>, RemoverPerfilPermissaoRepositorio>();

        // Funcionário
        services.AddScoped<ICadastrarRepositorio<tb04_funcionarioModel>, CadastrarFuncionarioRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb04_funcionarioModel>, ActualizarFuncionarioRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb04_funcionarioModel>, PesquisarPorIdFuncionarioRepositorio>();
        services.AddScoped<IPesquisarTelefoneRepositorio<tb04_funcionarioModel>, PesquisarPorTelefoneRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb04_funcionarioModel>, PesquisarTodosFuncionarioRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb04_funcionarioModel>, RemoverFuncionarioRepositorio>();
        services.AddScoped<ILoginRepositorio, LoginRepositorio>();

        // Credencial de acesso
        services.AddScoped<ICadastrarRepositorio<tb05_credencial_acessoModel>, CadastrarCredencialRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb05_credencial_acessoModel>, ActualizarCredencialRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb05_credencial_acessoModel>, PesquisarPorIdCredencialRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb05_credencial_acessoModel>, PesquisarPorTextoCredencialRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb05_credencial_acessoModel>, PesquisarTodosCredencialRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb05_credencial_acessoModel>, RemoverCredencialRepositorio>();

        // Cliente
        services.AddScoped<ICadastrarRepositorio<tb06_clienteModel>, CadastrarClienteRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb06_clienteModel>, ActualizarClienteRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb06_clienteModel>, PesquisarPorIdClienteRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb06_clienteModel>, PesquisarPorTextoClienteRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb06_clienteModel>, PesquisarTodosClienteRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb06_clienteModel>, RemoverClienteRepositorio>();

        // Telefone
        services.AddScoped<ICadastrarRepositorio<tb07_telefoneModel>, CadastrarTelefoneRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb07_telefoneModel>, ActualizarTelefoneRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb07_telefoneModel>, PesquisarPorIdTelefoneRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb07_telefoneModel>, PesquisarPorTextoTelefoneRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb07_telefoneModel>, PesquisarTodosTelefoneRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb07_telefoneModel>, RemoverTelefoneRepositorio>();

        // Email
        services.AddScoped<ICadastrarRepositorio<tb08_emailModel>, CadastrarEmailRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb08_emailModel>, ActualizarEmailRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb08_emailModel>, PesquisarPorIdEmailRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb08_emailModel>, PesquisarPorTextoEmailRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb08_emailModel>, PesquisarTodosEmailRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb08_emailModel>, RemoverEmailRepositorio>();

        // Tipo de imóvel
        services.AddScoped<ICadastrarRepositorio<tb09_tipo_imovelModel>, CadastrarTipoImovelRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb09_tipo_imovelModel>, ActualizarTipoImovelRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb09_tipo_imovelModel>, PesquisarPorIdTipoImovelRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb09_tipo_imovelModel>, PesquisarPorTextoTipoImovelRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb09_tipo_imovelModel>, PesquisarTodosTipoImovelRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb09_tipo_imovelModel>, RemoverTipoImovelRepositorio>();

        // Tipologia
        services.AddScoped<ICadastrarRepositorio<tb10_tipologiaModel>, CadastrarTipologiaRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb10_tipologiaModel>, ActualizarTipologiaRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb10_tipologiaModel>, PesquisarPorIdTipologiaRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb10_tipologiaModel>, PesquisarPorTextoTipologiaRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb10_tipologiaModel>, PesquisarTodosTipologiaRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb10_tipologiaModel>, RemoverTipologiaRepositorio>();

        // Imóvel
        services.AddScoped<ICadastrarRepositorio<tb11_imovelModel>, CadastrarImovelRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb11_imovelModel>, ActualizarImovelRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb11_imovelModel>, PesquisarPorIdImovelRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb11_imovelModel>, PesquisarPorTextoImovelRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb11_imovelModel>, PesquisarTodosImovelRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb11_imovelModel>, RemoverImovelRepositorio>();

        // Foto
        services.AddScoped<ICadastrarRepositorio<tb12_fotoModel>, CadastrarFotoRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb12_fotoModel>, ActualizarFotoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb12_fotoModel>, PesquisarPorIdFotoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb12_fotoModel>, PesquisarPorTextoFotoRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb12_fotoModel>, PesquisarTodosFotoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb12_fotoModel>, RemoverFotoRepositorio>();

        // Vídeo
        services.AddScoped<ICadastrarRepositorio<tb13_videoModel>, CadastrarVideoRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb13_videoModel>, ActualizarVideoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb13_videoModel>, PesquisarPorIdVideoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb13_videoModel>, PesquisarPorTextoVideoRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb13_videoModel>, PesquisarTodosVideoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb13_videoModel>, RemoverVideoRepositorio>();

        // Província
        services.AddScoped<ICadastrarRepositorio<tb14_provinciaModel>, CadastrarProvinciaRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb14_provinciaModel>, ActualizarProvinciaRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb14_provinciaModel>, PesquisarPorIdProvinciaRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb14_provinciaModel>, PesquisarPorTextoProvinciaRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb14_provinciaModel>, PesquisarTodosProvinciaRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb14_provinciaModel>, RemoverProvinciaRepositorio>();

        // Município
        services.AddScoped<ICadastrarRepositorio<tb15_municipioModel>, CadastrarMunicipioRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb15_municipioModel>, ActualizarMunicipioRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb15_municipioModel>, PesquisarPorIdMunicipioRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb15_municipioModel>, PesquisarPorTextoMunicipioRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb15_municipioModel>, PesquisarTodosMunicipioRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb15_municipioModel>, RemoverMunicipioRepositorio>();

        // Bairro
        services.AddScoped<ICadastrarRepositorio<tb16_bairroModel>, CadastrarBairroRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb16_bairroModel>, ActualizarBairroRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb16_bairroModel>, PesquisarPorIdBairroRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb16_bairroModel>, PesquisarPorTextoBairroRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb16_bairroModel>, PesquisarTodosBairroRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb16_bairroModel>, RemoverBairroRepositorio>();

        // Endereço
        services.AddScoped<ICadastrarRepositorio<tb17_enderecoModel>, CadastrarEnderecoRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb17_enderecoModel>, ActualizarEnderecoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb17_enderecoModel>, PesquisarPorIdEnderecoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb17_enderecoModel>, PesquisarPorTextoEnderecoRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb17_enderecoModel>, PesquisarTodosEnderecoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb17_enderecoModel>, RemoverEnderecoRepositorio>();

        // Proprietário
        services.AddScoped<ICadastrarRepositorio<tb18_proprietarioModel>, CadastrarProprietarioRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb18_proprietarioModel>, ActualizarProprietarioRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb18_proprietarioModel>, PesquisarPorIdProprietarioRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb18_proprietarioModel>, PesquisarPorTextoProprietarioRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb18_proprietarioModel>, PesquisarTodosProprietarioRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb18_proprietarioModel>, RemoverProprietarioRepositorio>();

        // Estado de solicitação
        services.AddScoped<ICadastrarRepositorio<tb19_estado_solicitacaoModel>, CadastrarEstadoSolicitacaoRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb19_estado_solicitacaoModel>, ActualizarEstadoSolicitacaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb19_estado_solicitacaoModel>, PesquisarPorIdEstadoSolicitacaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb19_estado_solicitacaoModel>, PesquisarPorTextoEstadoSolicitacaoRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb19_estado_solicitacaoModel>, PesquisarTodosEstadoSolicitacaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb19_estado_solicitacaoModel>, RemoverEstadoSolicitacaoRepositorio>();

        // Solicitação
        services.AddScoped<ICadastrarRepositorio<tb20_solicitacaoModel>, CadastrarSolicitacaoRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb20_solicitacaoModel>, ActualizarSolicitacaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb20_solicitacaoModel>, PesquisarPorIdSolicitacaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb20_solicitacaoModel>, PesquisarPorTextoSolicitacaoRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb20_solicitacaoModel>, PesquisarTodosSolicitacaoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb20_solicitacaoModel>, RemoverSolicitacaoRepositorio>();

        // Favorito
        services.AddScoped<ICadastrarRepositorio<tb21_favoritoModel>, CadastrarFavoritoRepositorio>();
        services.AddScoped<IActualizarRepositorio<tb21_favoritoModel>, ActualizarFavoritoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorIdRepositorio<tb21_favoritoModel>, PesquisarPorIdFavoritoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IPesquisarPorTextoRepositorio<tb21_favoritoModel>, PesquisarPorTextoFavoritoRepositorio>();
        services.AddScoped<IPesquisarTodosRepositorio<tb21_favoritoModel>, PesquisarTodosFavoritoRepositorio>();
        services.AddScoped<Corretora.C01.Domain.Interfaces.IRemoverRepositorio<tb21_favoritoModel>, RemoverFavoritoRepositorio>();


        return services;
    }
}




