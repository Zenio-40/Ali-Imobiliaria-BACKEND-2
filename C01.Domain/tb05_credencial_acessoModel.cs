using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("Credencial")]
public class tb05_credencial_acessoModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("tb04_funcionarioModel")]
    public int? Idtb04_funcionarioModel {get; set;}

    [Column("tb06_clienteModelId")]
    public int? Idtb06_clienteModel {get; set;}

    [Column("Senha_hash")]
    public string Senha_hash {get; set;} = string.Empty;

    [Column("Senha_salt")]
    public string Senha_salt {get; set;} = string.Empty;

    public tb04_funcionarioModel? Funcionario {get; set;}

    public tb06_clienteModel? Cliente {get; set;}

}
