using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("TipoImovel")]
public class tb09_tipo_imovelModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("Descricao")]
    public string Descricao {get; set;} = string.Empty;

    public ICollection<tb11_imovelModel> Imovel {get; set;} = [];

}
