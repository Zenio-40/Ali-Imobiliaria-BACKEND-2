using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Video")]
public class tb13_videoModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("tb11_imovelModel")]
    public int tb11_imovelModel {get; set;}

    [Column("TipoImovel")]
    public int tb09_tipo_imovel {get; set;}

    [Column("Video")]
    public string Video {get; set;} = string.Empty;

    public tb11_imovelModel Imovel {get; set;} = null!;
}
