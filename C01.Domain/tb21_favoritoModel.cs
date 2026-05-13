using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Favorito")]
public class tb21_favoritoModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("Cliente")]
    public int tb06_clienteModel {get; set;}

    [Column("Imovel")]
    public int tb11_imovelModel {get; set;}

    public tb06_clienteModel Cliente {get; set;} = null!;

    public tb11_imovelModel Imovel {get; set;} = null!;
}
