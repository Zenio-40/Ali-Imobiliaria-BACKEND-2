using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;

/// <summary>
/// Entidade tb03_PerfilPermissao - Tabela de junção para o relacionamento Muitos-para-Muitos
/// entre tb02_Perfil e tb01_Permissao
/// </summary>
[Table("PerfilPermissao")]
public class tb03_perfiL_permissaoModel
{
    [Column("id")]
    [Key]
    public int id { get; set; }

    [Column("IdPerfil")]
    [Required(ErrorMessage = "O ID do perfil é obrigatório")]
    [ForeignKey(nameof(Perfil))]
    public int Idtb02_perfilModel { get; set; }

    [Column("IdPermissao")]
    [Required(ErrorMessage = "O ID da permissão é obrigatório")]
    [ForeignKey(nameof(Permissao))]
    public int Idtb01_permissaoModel { get; set; }

    [Column("Estado")]
    [Required(ErrorMessage = "O estado é obrigatório")]
    public bool Estado { get; set; } = true;

    [Column("DataCriacao")]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    [Column("DataAtualizacao")]
    public DateTime? DataAtualizacao { get; set; }

    // Relacionamentos de Navegação
    /// <summary>
    /// Referência para a entidade de Permissão
    /// </summary>
    public tb01_permissaoModel Permissao { get; set; } = null!;

    /// <summary>
    /// Referência para a entidade de Perfil
    /// </summary>
    public tb02_perfilModel Perfil { get; set; } = null!;
}

