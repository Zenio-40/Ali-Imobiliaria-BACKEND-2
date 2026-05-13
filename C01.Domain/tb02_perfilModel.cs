using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;

/// <summary>
/// Entidade tb02_Perfil - Define os perfis de usuário (Admin, Gerente, Operador, etc)
/// Relacionamento: Muitos-para-Muitos com tb01_Permissao através de tb03_perfiL_permissaoModel
/// Relacionamento: Um-para-Muitos com tb04_Funcionario
/// Relacionamento: Um-para-Muitos com tb06_Cliente
/// </summary>
[Table("Perfil")]
public class tb02_perfilModel
{
    [Column("id")]
    [Key]
    public int id { get; set; }

    [Column("descricao")]
    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 100 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Column("DataCriacao")]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    [Column("DataAtualizacao")]
    public DateTime? DataAtualizacao { get; set; }

    [Column("Estado")]
    public bool Estado { get; set; } = true;

    // Relacionamento Muitos-para-Muitos com Permissao
    /// <summary>
    /// Coleção de relacionamentos entre Perfil e Permissão
    /// </summary>
    public ICollection<tb03_perfiL_permissaoModel> PerfilPermissao { get; set; } = [];

    // Relacionamento Um-para-Muitos com Funcionário
    /// <summary>
    /// Coleção de funcionários que possuem este perfil
    /// </summary>
    public ICollection<tb04_funcionarioModel> Funcionarios { get; set; } = [];

    // Relacionamento Um-para-Muitos com Cliente
    /// <summary>
    /// Coleção de clientes que possuem este perfil
    /// </summary>
    public ICollection<tb06_clienteModel> Clientes { get; set; } = [];
}
