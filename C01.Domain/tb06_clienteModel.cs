using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;

/// <summary>
/// Entidade tb06_Cliente - Representa os clientes do sistema
/// Relacionamento: Um-para-Um com tb02_Perfil (cada cliente tem um perfil)
/// Relacionamento: Um-para-Muitos com tb07_Telefone
/// Relacionamento: Um-para-Muitos com tb08_Email
/// Relacionamento: Um-para-Muitos com tb21_Favorito
/// Relacionamento: Um-para-Muitos com tb20_Solicitacao
/// </summary>
[Table("Cliente")]
public class tb06_clienteModel
{
    [Column("Id")]
    [Key]
    public int Id { get; set; }

    [Column("IdPerfil")]
    [Required(ErrorMessage = "O perfil do cliente é obrigatório")]
    [ForeignKey(nameof(Perfil))]
    public int Idtb02_perfilModel { get; set; }

    [Column("Nome")]
    [Required(ErrorMessage = "O nome do cliente é obrigatório")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Column("DataCriacao")]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    [Column("DataAtualizacao")]
    public DateTime? DataAtualizacao { get; set; }

    [Column("Estado")]
    [Required(ErrorMessage = "O estado é obrigatório")]

    public bool Estado { get; set; } = true;

    // Relacionamento Um-para-Um com Perfil
    /// <summary>
    /// Referência para o perfil do cliente
    /// </summary>
    public tb02_perfilModel Perfil { get; set; } = null!;

    // ✅ Adicione no tb06_clienteModel
public ICollection<tb05_credencial_acessoModel> Credencial { get; set; } = [];

    // Relacionamentos Um-para-Muitos
    public ICollection<tb07_telefoneModel> Telefone { get; set; } = [];

    public ICollection<tb08_emailModel> Email { get; set; } = [];

    public ICollection<tb21_favoritoModel> Favoritos { get; set; } = [];

    public ICollection<tb20_solicitacaoModel> Solicitacoes { get; set; } = [];
}


