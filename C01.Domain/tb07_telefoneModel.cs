using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Telefone")]
public class tb07_telefoneModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("tb06_clienteModel")]
    public int? tb06_clienteModel {get; set;}

    [Column("tb04_funcionarioModel")]
    public int? tb04_funcionarioModel {get; set;}

    [Column("Numero")]
    public string Numero {get; set;} = string.Empty;

    public tb04_funcionarioModel? Funcionario {get; set;}
    public tb06_clienteModel? Cliente {get; set;}
}
