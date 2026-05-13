using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Municipio")]
public class tb15_municipioModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("Provincia")]
    public int tb14_provinciaModel {get; set;}

    [Column("Nome")]
    public string Nome {get; set;} = string.Empty;

    public ICollection<tb16_bairroModel> Bairro {get; set;} = [];

    public tb14_provinciaModel Provincia {get; set;} = null!;
}
