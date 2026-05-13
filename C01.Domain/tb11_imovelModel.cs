using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Imovel")]
public class tb11_imovelModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("tb04_funcionarioModel")]
    public int tb04_funcionarioModel {get; set;}

    [Column("tb09_tipo_imovelModel")]
    public int tb09_tipo_imovelModel {get; set;}

    [Column("tb010_tipologiaModel")]
    public int tb10_tipologiaModel {get; set;}

    [Column("tb18_proprietarioModel")]
    public int? tb18_proprietarioModel {get; set;}

    [Column("Descricao")]
    public string Descricao {get; set;} = string.Empty;

    [Column("Preco")]
    public decimal Preco {get; set;}

    [Column("Estado")]
    public bool Estado {get; set;}

    [Column("EstadoAprovacao")]
    public string EstadoAprovacao {get; set;} = "Pendente";
    
    public ICollection<tb20_solicitacaoModel> Solicitacao {get; set;} = [];
    public ICollection<tb12_fotoModel> Foto {get; set;} = [];
    public ICollection<tb13_videoModel> Video {get; set;} = [];
    public ICollection<tb17_enderecoModel> Endereco {get; set;} = [];
    public ICollection<tb21_favoritoModel> Favorito {get; set;} = [];
    public tb04_funcionarioModel Funcionario {get; set;} = null!;
    public tb09_tipo_imovelModel TipoImovel {get; set;} = null!;
    public tb10_tipologiaModel Tipologia {get; set;} = null!;

    public tb18_proprietarioModel? ProprietarioModel {get; set;}
}
