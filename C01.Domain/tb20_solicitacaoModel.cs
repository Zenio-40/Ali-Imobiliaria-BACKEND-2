using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Solicitacao")]
public class tb20_solicitacaoModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("Cliente")]
    public int tb06_clienteModel {get; set;}

    [Column("Imovel")]
    public int tb11_imovelModel {get; set;}

    [Column("EstadoSolicitacao")]
    public int tb19_estado_solicitacaoModel {get; set;}

    [Column("Data")]
    public DateTime Data { get; set; }

    public tb06_clienteModel Cliente {get; set;} = null!;

    public tb11_imovelModel Imovel {get; set;} = null!;

    public tb19_estado_solicitacaoModel EstadoSolicitacao {get; set;} = null!;
}
