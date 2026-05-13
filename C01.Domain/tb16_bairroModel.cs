using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Bairro")]
public class tb16_bairroModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("Municipio")]
    public int tb15_municipioModel {get; set;}

    [Column("Nome")]
    public string Nome {get; set;} = string.Empty;

    public tb15_municipioModel Municipio {get; set;} = null!;

    public ICollection<tb17_enderecoModel> Endereco {get; set;} = [];
}
