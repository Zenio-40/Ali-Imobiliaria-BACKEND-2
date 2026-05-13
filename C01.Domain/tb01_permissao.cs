using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;

/// <summary>
/// Entidade tb01_Permissao - Define as permissões do sistema
/// Relacionamento: Muitos-para-Muitos com tb02_Perfil através de tb03_perfiL_permissaoModel
/// </summary>
[Table("Permissao")]
public class tb01_permissaoModel
{
    [Column("id")]
    [Key]
    public int id { get; set; }

    [Column("descricao")]
    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 150 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Column("DataCriacao")]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    [Column("DataAtualizacao")]
    public DateTime? DataAtualizacao { get; set; }

    [Column("Estado")]
    public bool Estado { get; set; } = true;

    // Relacionamento Muitos-para-Muitos com Perfil
    /// <summary>
    /// Coleção de relacionamentos entre Permissão e Perfil
    /// </summary>
    public ICollection<tb03_perfiL_permissaoModel> PerfilPermissao { get; set; } = [];

    public ICollection<tb04_funcionarioModel> Funcionario { get; set; } = [];
}
