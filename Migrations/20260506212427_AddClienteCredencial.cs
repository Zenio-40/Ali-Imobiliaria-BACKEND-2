using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Corretora.Migrations
{
    /// <inheritdoc />
    public partial class AddClienteCredencial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoSolicitacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoSolicitacao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoImovel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoImovel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipologia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipologia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPerfil = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Perfil",
                        column: x => x.IdPerfil,
                        principalTable: "Perfil",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPerfil = table.Column<int>(type: "integer", nullable: false),
                    Numero = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    tb01_permissaoModelid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionario_Perfil",
                        column: x => x.IdPerfil,
                        principalTable: "Perfil",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_funcionario_Permissao_tb01_permissaoModelid",
                        column: x => x.tb01_permissaoModelid,
                        principalTable: "Permissao",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PerfilPermissao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPerfil = table.Column<int>(type: "integer", nullable: false),
                    IdPermissao = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilPermissao", x => x.id);
                    table.ForeignKey(
                        name: "FK_PerfilPermissao_Perfil",
                        column: x => x.IdPerfil,
                        principalTable: "Perfil",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilPermissao_Permissao",
                        column: x => x.IdPermissao,
                        principalTable: "Permissao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Provincia = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipio_Provincia_Provincia",
                        column: x => x.Provincia,
                        principalTable: "Provincia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credencial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tb04_funcionarioModel = table.Column<int>(type: "integer", nullable: false),
                    Senha_hash = table.Column<string>(type: "text", nullable: false),
                    Senha_salt = table.Column<string>(type: "text", nullable: false),
                    tb06_clienteModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credencial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credencial_Cliente_tb06_clienteModelId",
                        column: x => x.tb06_clienteModelId,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Credencial_funcionario_tb04_funcionarioModel",
                        column: x => x.tb04_funcionarioModel,
                        principalTable: "funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tb06_clienteModel = table.Column<int>(type: "integer", nullable: false),
                    tb04_funcionarioMOdel = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Email_Cliente_tb06_clienteModel",
                        column: x => x.tb06_clienteModel,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Email_funcionario_tb04_funcionarioMOdel",
                        column: x => x.tb04_funcionarioMOdel,
                        principalTable: "funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tb06_clienteModel = table.Column<int>(type: "integer", nullable: false),
                    tb04_funcionarioModel = table.Column<int>(type: "integer", nullable: false),
                    Numero = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefone_Cliente_tb06_clienteModel",
                        column: x => x.tb06_clienteModel,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Telefone_funcionario_tb04_funcionarioModel",
                        column: x => x.tb04_funcionarioModel,
                        principalTable: "funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bairro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Municipio = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bairro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bairro_Municipio_Municipio",
                        column: x => x.Municipio,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tb11_imovelModel = table.Column<int>(type: "integer", nullable: false),
                    TipoImovel = table.Column<int>(type: "integer", nullable: false),
                    Bairro = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Bairro_Bairro",
                        column: x => x.Bairro,
                        principalTable: "Bairro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cliente = table.Column<int>(type: "integer", nullable: false),
                    Imovel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorito_Cliente_Cliente",
                        column: x => x.Cliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tb11_imovelModel = table.Column<int>(type: "integer", nullable: false),
                    TipoImovel = table.Column<int>(type: "integer", nullable: false),
                    Foto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Imovel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tb04_funcionarioModel = table.Column<int>(type: "integer", nullable: false),
                    tb09_tipo_imovelModel = table.Column<int>(type: "integer", nullable: false),
                    tb010_tipologiaModel = table.Column<int>(type: "integer", nullable: false),
                    tb18_proprietarioModel = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imovel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imovel_TipoImovel_tb09_tipo_imovelModel",
                        column: x => x.tb09_tipo_imovelModel,
                        principalTable: "TipoImovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Imovel_Tipologia_tb010_tipologiaModel",
                        column: x => x.tb010_tipologiaModel,
                        principalTable: "Tipologia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Imovel_funcionario_tb04_funcionarioModel",
                        column: x => x.tb04_funcionarioModel,
                        principalTable: "funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proprietario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoImovel = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    tb11_imovelModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proprietario_Imovel_tb11_imovelModelId",
                        column: x => x.tb11_imovelModelId,
                        principalTable: "Imovel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cliente = table.Column<int>(type: "integer", nullable: false),
                    Imovel = table.Column<int>(type: "integer", nullable: false),
                    EstadoSolicitacao = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Cliente_Cliente",
                        column: x => x.Cliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitacao_EstadoSolicitacao_EstadoSolicitacao",
                        column: x => x.EstadoSolicitacao,
                        principalTable: "EstadoSolicitacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Imovel_Imovel",
                        column: x => x.Imovel,
                        principalTable: "Imovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tb11_imovelModel = table.Column<int>(type: "integer", nullable: false),
                    TipoImovel = table.Column<int>(type: "integer", nullable: false),
                    Video = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Imovel_tb11_imovelModel",
                        column: x => x.tb11_imovelModel,
                        principalTable: "Imovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EstadoSolicitacao",
                columns: new[] { "id", "descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Solicitação pendente", "Pendente" },
                    { 2, "Solicitação aprovada", "Aprovada" },
                    { 3, "Solicitação rejeitada", "Rejeitada" }
                });

            migrationBuilder.InsertData(
                table: "Perfil",
                columns: new[] { "id", "DataAtualizacao", "DataCriacao", "descricao", "Estado" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Admin", true },
                    { 2, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Corretor", true },
                    { 3, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Cliente", true }
                });

            migrationBuilder.InsertData(
                table: "Permissao",
                columns: new[] { "id", "DataAtualizacao", "DataCriacao", "descricao", "Estado" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Cadastrar Funcionário", true },
                    { 2, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Editar Funcionário", true },
                    { 3, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Desativar Funcionário", true },
                    { 4, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Listar Funcionários", true },
                    { 5, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Cadastrar Cliente", true },
                    { 6, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Editar Cliente", true },
                    { 7, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Listar Clientes", true },
                    { 8, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Cadastrar Imóvel", true },
                    { 9, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Editar Imóvel", true },
                    { 10, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Desativar Imóvel", true },
                    { 11, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Listar Imóveis", true }
                });

            migrationBuilder.InsertData(
                table: "Provincia",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Luanda" });

            migrationBuilder.InsertData(
                table: "TipoImovel",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "Apartamento" },
                    { 2, "Casa" },
                    { 3, "Terreno" }
                });

            migrationBuilder.InsertData(
                table: "Tipologia",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "T1" },
                    { 2, "T2" },
                    { 3, "T3" }
                });

            migrationBuilder.InsertData(
                table: "Municipio",
                columns: new[] { "Id", "Nome", "Provincia" },
                values: new object[] { 1, "Luanda", 1 });

            migrationBuilder.InsertData(
                table: "PerfilPermissao",
                columns: new[] { "id", "DataAtualizacao", "DataCriacao", "Estado", "IdPermissao", "IdPerfil" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 1, 1 },
                    { 2, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, 1 },
                    { 3, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, 1 },
                    { 4, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 4, 1 },
                    { 5, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, 1 },
                    { 6, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, 1 },
                    { 7, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 7, 1 },
                    { 8, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, 1 },
                    { 9, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, 1 },
                    { 10, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 10, 1 },
                    { 11, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 11, 1 },
                    { 12, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, 2 },
                    { 13, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, 2 },
                    { 14, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 7, 2 },
                    { 15, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, 2 },
                    { 16, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, 2 },
                    { 17, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 10, 2 },
                    { 18, null, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), true, 11, 2 }
                });

            migrationBuilder.InsertData(
                table: "Bairro",
                columns: new[] { "Id", "Nome", "Municipio" },
                values: new object[] { 1, "Centro", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Bairro_Municipio",
                table: "Bairro",
                column: "Municipio");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdPerfil",
                table: "Cliente",
                column: "IdPerfil");

            migrationBuilder.CreateIndex(
                name: "IX_Credencial_tb04_funcionarioModel",
                table: "Credencial",
                column: "tb04_funcionarioModel");

            migrationBuilder.CreateIndex(
                name: "IX_Credencial_tb06_clienteModelId",
                table: "Credencial",
                column: "tb06_clienteModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_tb04_funcionarioMOdel",
                table: "Email",
                column: "tb04_funcionarioMOdel");

            migrationBuilder.CreateIndex(
                name: "IX_Email_tb06_clienteModel",
                table: "Email",
                column: "tb06_clienteModel");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_Bairro",
                table: "Endereco",
                column: "Bairro");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_tb11_imovelModel",
                table: "Endereco",
                column: "tb11_imovelModel");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_Cliente",
                table: "Favorito",
                column: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_Imovel",
                table: "Favorito",
                column: "Imovel");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_tb11_imovelModel",
                table: "Foto",
                column: "tb11_imovelModel");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_IdPerfil",
                table: "funcionario",
                column: "IdPerfil");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_tb01_permissaoModelid",
                table: "funcionario",
                column: "tb01_permissaoModelid");

            migrationBuilder.CreateIndex(
                name: "IX_Imovel_tb010_tipologiaModel",
                table: "Imovel",
                column: "tb010_tipologiaModel");

            migrationBuilder.CreateIndex(
                name: "IX_Imovel_tb04_funcionarioModel",
                table: "Imovel",
                column: "tb04_funcionarioModel");

            migrationBuilder.CreateIndex(
                name: "IX_Imovel_tb09_tipo_imovelModel",
                table: "Imovel",
                column: "tb09_tipo_imovelModel");

            migrationBuilder.CreateIndex(
                name: "IX_Imovel_tb18_proprietarioModel",
                table: "Imovel",
                column: "tb18_proprietarioModel");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_Provincia",
                table: "Municipio",
                column: "Provincia");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilPermissao_IdPerfil",
                table: "PerfilPermissao",
                column: "IdPerfil");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilPermissao_IdPermissao",
                table: "PerfilPermissao",
                column: "IdPermissao");

            migrationBuilder.CreateIndex(
                name: "IX_Proprietario_tb11_imovelModelId",
                table: "Proprietario",
                column: "tb11_imovelModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_Cliente",
                table: "Solicitacao",
                column: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_EstadoSolicitacao",
                table: "Solicitacao",
                column: "EstadoSolicitacao");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_Imovel",
                table: "Solicitacao",
                column: "Imovel");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_tb04_funcionarioModel",
                table: "Telefone",
                column: "tb04_funcionarioModel");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_tb06_clienteModel",
                table: "Telefone",
                column: "tb06_clienteModel");

            migrationBuilder.CreateIndex(
                name: "IX_Video_tb11_imovelModel",
                table: "Video",
                column: "tb11_imovelModel");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Imovel_tb11_imovelModel",
                table: "Endereco",
                column: "tb11_imovelModel",
                principalTable: "Imovel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorito_Imovel_Imovel",
                table: "Favorito",
                column: "Imovel",
                principalTable: "Imovel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Imovel_tb11_imovelModel",
                table: "Foto",
                column: "tb11_imovelModel",
                principalTable: "Imovel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Imovel_Proprietario_tb18_proprietarioModel",
                table: "Imovel",
                column: "tb18_proprietarioModel",
                principalTable: "Proprietario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Perfil",
                table: "funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_Imovel_funcionario_tb04_funcionarioModel",
                table: "Imovel");

            migrationBuilder.DropForeignKey(
                name: "FK_Proprietario_Imovel_tb11_imovelModelId",
                table: "Proprietario");

            migrationBuilder.DropTable(
                name: "Credencial");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Favorito");

            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "PerfilPermissao");

            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Bairro");

            migrationBuilder.DropTable(
                name: "EstadoSolicitacao");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "Provincia");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropTable(
                name: "funcionario");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropTable(
                name: "Imovel");

            migrationBuilder.DropTable(
                name: "Proprietario");

            migrationBuilder.DropTable(
                name: "TipoImovel");

            migrationBuilder.DropTable(
                name: "Tipologia");
        }
    }
}
