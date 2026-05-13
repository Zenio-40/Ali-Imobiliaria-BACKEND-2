using Corretora.C01.Domain;
using Microsoft.EntityFrameworkCore;

namespace Corretora.C03.Infra.Data;

public class CorretoraDbContext(DbContextOptions<CorretoraDbContext> options) : DbContext(options)
{
    public DbSet<tb01_permissaoModel> Tabela01Permissao {get; set;}

    public DbSet<tb02_perfilModel> Tabela02Perfil {get; set;}

    public DbSet<tb03_perfiL_permissaoModel> Tabela03PerfilPermissao {get; set;}

    public DbSet<tb04_funcionarioModel> Tabela04Funcinario {get; set;}

    public DbSet<tb05_credencial_acessoModel> Tabela05Credencial {get; set;}

    public DbSet<tb06_clienteModel> Tabela06Cliente {get; set;}

    public DbSet<tb07_telefoneModel> Tabela07Telefone {get; set;}

    public DbSet<tb08_emailModel> Tabela08Email {get; set;}

    public DbSet<tb09_tipo_imovelModel> Tabela09TipolaImovel {get; set;}

    public DbSet<tb10_tipologiaModel> Tabela10Tipologia {get; set;}

    public DbSet<tb11_imovelModel> Tabela11Imovel {get; set;}

    public DbSet<tb12_fotoModel> Tabela12Foto {get; set;}

    public DbSet<tb13_videoModel> Tabela13Video {get; set;}

    public DbSet<tb14_provinciaModel> Tabela14Pronvincia {get; set;}

    public DbSet<tb15_municipioModel> Tabela15Municipio {get; set;}

    public DbSet<tb16_bairroModel> Tabela16Bairro {get; set;}

    public DbSet<tb17_enderecoModel> Tabela17Enderco {get; set;}
    
    public DbSet<tb18_proprietarioModel> Tabela18Proprietario {get; set;}

    public DbSet<tb19_estado_solicitacaoModel> Tabela19EstadoSolicitacao {get; set;}

    public DbSet<tb20_solicitacaoModel> Tabela20Solicitacao {get; set;}

    public DbSet<tb21_favoritoModel> Tabela21Favorito {get; set;}



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ========== CONFIGURAÇÕES DE PERMISSÃO E PERFIL ==========
        
        /// <summary>
        /// Configuração da relação Muitos-para-Muitos entre tb02_PerfilModel e tb01_PermissaoModel
        /// através da tabela de junção tb03_PerfilPermissaoModel
        /// </summary>
        modelBuilder.Entity<tb03_perfiL_permissaoModel>(entity =>
        {
            // Chave primária composta (opcional, mas recomendado para tabelas de junção)
            entity.HasKey(e => e.id);

            // Relação: Muitos PerfilPermissoes para Um Permissao
            entity.HasOne(e => e.Permissao)
                .WithMany(p => p.PerfilPermissao)
                .HasForeignKey(e => e.Idtb01_permissaoModel)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PerfilPermissao_Permissao");

            // Relação: Muitos PerfilPermissoes para Um Perfil
            entity.HasOne(e => e.Perfil)
                .WithMany(p => p.PerfilPermissao)
                .HasForeignKey(e => e.Idtb02_perfilModel)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PerfilPermissao_Perfil");
        });

        // ========== CONFIGURAÇÕES DE FUNCIONÁRIO ==========

        /// <summary>
        /// Configuração da relação Um-para-Muitos entre tb02_PerfilModel e tb04_FuncionarioModel
        /// Um Perfil possui muitos Funcionários
        /// </summary>
        modelBuilder.Entity<tb04_funcionarioModel>(entity =>
        {
            entity.HasOne(e => e.Perfil)
                .WithMany(p => p.Funcionarios)
                .HasForeignKey(e => e.Idtb02_perfilModel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Funcionario_Perfil");
        });

        // ========== CONFIGURAÇÕES DE CLIENTE ==========

        /// <summary>
        /// Configuração da relação Um-para-Muitos entre tb02_PerfilModel e tb06_ClienteModel
        /// Um Perfil possui muitos Clientes
        /// </summary>
        modelBuilder.Entity<tb06_clienteModel>(entity =>
        {
            entity.HasOne(e => e.Perfil)
                .WithMany(p => p.Clientes)
                .HasForeignKey(e => e.Idtb02_perfilModel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Cliente_Perfil");
        });

        modelBuilder.Entity<tb05_credencial_acessoModel>(entity =>
        {
            entity.HasOne(e => e.Funcionario)
                .WithMany(f => f.Credencial)
                .HasForeignKey(e => e.Idtb04_funcionarioModel)
                .IsRequired(false);

            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Credencial)
                .HasForeignKey(e => e.Idtb06_clienteModel)
                .IsRequired(false);
        });

        modelBuilder.Entity<tb07_telefoneModel>(entity =>
        {
            entity.HasOne(e => e.Funcionario)
                .WithMany(f => f.Telefone)
                .HasForeignKey(e => e.tb04_funcionarioModel)
                .IsRequired(false);

            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Telefone)
                .HasForeignKey(e => e.tb06_clienteModel)
                .IsRequired(false);
        });

        modelBuilder.Entity<tb08_emailModel>(entity =>
        {
            entity.HasOne(e => e.Funcionario)
                .WithMany(f => f.Email)
                .HasForeignKey(e => e.tb04_funcionarioModel)
                .IsRequired(false);

            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Email)
                .HasForeignKey(e => e.tb06_clienteModel)
                .IsRequired(false);
        });

        modelBuilder.Entity<tb11_imovelModel>(entity =>
        {
            entity.HasOne(e => e.Funcionario)
                .WithMany(f => f.Imovel)
                .HasForeignKey(e => e.tb04_funcionarioModel);

            entity.HasOne(e => e.TipoImovel)
                .WithMany(t => t.Imovel)
                .HasForeignKey(e => e.tb09_tipo_imovelModel);

            entity.HasOne(e => e.Tipologia)
                .WithMany(t => t.Imovel)
                .HasForeignKey(e => e.tb10_tipologiaModel);

            entity.HasOne(e => e.ProprietarioModel)
                .WithMany(p => p.Imoveis)
                .HasForeignKey(e => e.tb18_proprietarioModel)
                .IsRequired(false);
        });

        modelBuilder.Entity<tb12_fotoModel>(entity =>
        {
            entity.HasOne(e => e.Imovel)
                .WithMany(i => i.Foto)
                .HasForeignKey(e => e.tb11_imovelModel);
        });

        modelBuilder.Entity<tb13_videoModel>(entity =>
        {
            entity.HasOne(e => e.Imovel)
                .WithMany(i => i.Video)
                .HasForeignKey(e => e.tb11_imovelModel);
        });

        modelBuilder.Entity<tb17_enderecoModel>(entity =>
        {
            entity.HasOne(e => e.Imovel)
                .WithMany(i => i.Endereco)
                .HasForeignKey(e => e.tb11_imovelModel);

            entity.HasOne(e => e.Bairro)
                .WithMany(b => b.Endereco)
                .HasForeignKey(e => e.tb16_bairroModel);
        });

        modelBuilder.Entity<tb15_municipioModel>(entity =>
        {
            entity.HasOne(e => e.Provincia)
                .WithMany(p => p.Municipio)
                .HasForeignKey(e => e.tb14_provinciaModel);
        });

        modelBuilder.Entity<tb16_bairroModel>(entity =>
        {
            entity.HasOne(e => e.Municipio)
                .WithMany(m => m.Bairro)
                .HasForeignKey(e => e.tb15_municipioModel);
        });

        modelBuilder.Entity<tb20_solicitacaoModel>(entity =>
        {
            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Solicitacoes)
                .HasForeignKey(e => e.tb06_clienteModel);

            entity.HasOne(e => e.Imovel)
                .WithMany(i => i.Solicitacao)
                .HasForeignKey(e => e.tb11_imovelModel);

            entity.HasOne(e => e.EstadoSolicitacao)
                .WithMany(s => s.Solicitacao)
                .HasForeignKey(e => e.tb19_estado_solicitacaoModel);
        });

        modelBuilder.Entity<tb21_favoritoModel>(entity =>
        {
            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Favoritos)
                .HasForeignKey(e => e.tb06_clienteModel);

            entity.HasOne(e => e.Imovel)
                .WithMany(i => i.Favorito)
                .HasForeignKey(e => e.tb11_imovelModel);
        });

        // ========== SEED DATA ==========

        // Permissões do Sistema
        modelBuilder.Entity<tb01_permissaoModel>().HasData(
            new tb01_permissaoModel 
            { 
                id = 1, 
                Descricao = "Cadastrar Funcionário",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 2, 
                Descricao = "Editar Funcionário",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 3, 
                Descricao = "Desativar Funcionário",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 4, 
                Descricao = "Listar Funcionários",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 5, 
                Descricao = "Cadastrar Cliente",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 6, 
                Descricao = "Editar Cliente",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 7, 
                Descricao = "Listar Clientes",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 8, 
                Descricao = "Cadastrar Imóvel",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 9, 
                Descricao = "Editar Imóvel",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 10, 
                Descricao = "Desativar Imóvel",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb01_permissaoModel 
            { 
                id = 11, 
                Descricao = "Listar Imóveis",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Perfis do Sistema
        modelBuilder.Entity<tb02_perfilModel>().HasData(
            new tb02_perfilModel 
            { 
                id = 1, 
                Descricao = "Admin",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb02_perfilModel 
            { 
                id = 2, 
                Descricao = "Corretor",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb02_perfilModel 
            { 
                id = 3, 
                Descricao = "Cliente",
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Associações Perfil-Permissão
        modelBuilder.Entity<tb03_perfiL_permissaoModel>().HasData(
            // ========== ADMIN: Todas as 11 permissões ==========
            new tb03_perfiL_permissaoModel 
            { 
                id = 1, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 1, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 2, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 2, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 3, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 3, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 4, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 4, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 5, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 5, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 6, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 6, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 7, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 7, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 8, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 8, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 9, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 9, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 10, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 10, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 11, 
                Idtb02_perfilModel = 1, 
                Idtb01_permissaoModel = 11, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // ========== CORRETOR: Permissões 5-11 (Cliente e Imóvel) ==========
            new tb03_perfiL_permissaoModel 
            { 
                id = 12, 
                Idtb02_perfilModel = 2, 
                Idtb01_permissaoModel = 5, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 13, 
                Idtb02_perfilModel = 2, 
                Idtb01_permissaoModel = 6, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 14, 
                Idtb02_perfilModel = 2, 
                Idtb01_permissaoModel = 7, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 15, 
                Idtb02_perfilModel = 2, 
                Idtb01_permissaoModel = 8, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 16, 
                Idtb02_perfilModel = 2, 
                Idtb01_permissaoModel = 9, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 17, 
                Idtb02_perfilModel = 2, 
                Idtb01_permissaoModel = 10, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            },
            new tb03_perfiL_permissaoModel 
            { 
                id = 18, 
                Idtb02_perfilModel = 2, 
                Idtb01_permissaoModel = 11, 
                Estado = true,
                DataCriacao = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<tb14_provinciaModel>().HasData(
            new tb14_provinciaModel { Id = 1, Nome = "Luanda" }
        );

        modelBuilder.Entity<tb15_municipioModel>().HasData(
            new tb15_municipioModel { Id = 1, Nome = "Luanda", tb14_provinciaModel = 1 }
        );

        modelBuilder.Entity<tb16_bairroModel>().HasData(
            new tb16_bairroModel { Id = 1, Nome = "Centro", tb15_municipioModel = 1 }
        );

        modelBuilder.Entity<tb09_tipo_imovelModel>().HasData(
            new tb09_tipo_imovelModel { Id = 1, Descricao = "Apartamento" },
            new tb09_tipo_imovelModel { Id = 2, Descricao = "Casa" },
            new tb09_tipo_imovelModel { Id = 3, Descricao = "Terreno" }
        );

        modelBuilder.Entity<tb10_tipologiaModel>().HasData(
            new tb10_tipologiaModel { Id = 1, Descricao = "T1" },
            new tb10_tipologiaModel { Id = 2, Descricao = "T2" },
            new tb10_tipologiaModel { Id = 3, Descricao = "T3" }
        );

        modelBuilder.Entity<tb19_estado_solicitacaoModel>().HasData(
            new tb19_estado_solicitacaoModel { Id = 1, Nome = "Pendente", Descricao = "Solicitação pendente" },
            new tb19_estado_solicitacaoModel { Id = 2, Nome = "Aprovada", Descricao = "Solicitação aprovada" },
            new tb19_estado_solicitacaoModel { Id = 3, Nome = "Rejeitada", Descricao = "Solicitação rejeitada" }
        );
    }

}
