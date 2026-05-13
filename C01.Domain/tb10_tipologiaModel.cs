using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Tipologia")]
public class tb10_tipologiaModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("Descricao")]
    public string Descricao {get; set;} = string.Empty;

    public ICollection<tb11_imovelModel> Imovel {get; set;} = [];
}
