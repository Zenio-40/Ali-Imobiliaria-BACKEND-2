using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Provincia")]
public class tb14_provinciaModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("Nome")]
    public string Nome {get; set;} = string.Empty;

    public ICollection<tb15_municipioModel> Municipio {get; set;} = [];
}
