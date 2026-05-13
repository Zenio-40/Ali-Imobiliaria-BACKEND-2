using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Foto")]
public class tb12_fotoModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("tb11_imovelModel")]
    public int tb11_imovelModel {get; set;}

    [Column("TipoImovel")]
    public int tb09_tipo_imovel {get; set;}

    [Column("Foto")]
    public string Foto {get; set;} = string.Empty;

    public tb11_imovelModel Imovel {get; set;} = null!;
}

