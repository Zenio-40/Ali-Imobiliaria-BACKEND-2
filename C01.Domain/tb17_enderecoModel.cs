using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Endereco")]
public class tb17_enderecoModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("tb11_imovelModel")]
    public int tb11_imovelModel {get; set;}

    [Column("TipoImovel")]
    public int tb09_tipo_imovelModel {get; set;}

    [Column("Bairro")]
    public int tb16_bairroModel {get; set;}

    [Column("Nome")]
    public string Nome {get; set;} = string.Empty;

    public tb11_imovelModel Imovel {get; set;} = null!;
    public tb16_bairroModel Bairro {get; set;} = null!;
}
