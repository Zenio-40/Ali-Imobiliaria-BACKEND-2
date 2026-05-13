using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Proprietario")]
public class tb18_proprietarioModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("TipoImovel")]
    public int tb09_tipo_imovelModel {get; set;}

    [Column("Nome")]
    public string Nome {get; set;} = string.Empty;

    public string Telefone {get; set;} = string.Empty;

    public ICollection<tb11_imovelModel> Imoveis {get; set;} = [];
}

