using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("EstadoSolicitacao")]
public class tb19_estado_solicitacaoModel
{
    [Column("id")]
    public int Id {get; set;}

    [Column("Nome")]
    public string Nome {get; set;} = string.Empty;

    [Column("descricao")]
    public string Descricao {get; set;} = string.Empty;

    public ICollection<tb20_solicitacaoModel> Solicitacao {get; set;} = [];

}
