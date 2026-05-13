using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corretora.C01.Domain;
[Table("funcionario")]
public class tb04_funcionarioModel
{
    [Column("Id")]
    public int Id {get; set;}

    [Column("IdPerfil")]
    public int Idtb02_perfilModel {get; set;}

    [Column("Numero")]
    public string Numero {get; set;} = string.Empty;

    [Column("Nome")]
    public string Nome {get; set;} = string.Empty;

    [Column("Estado")]
    public bool Estado {get; set;}

    public ICollection<tb07_telefoneModel> Telefone {get; set;} = [];

    public ICollection<tb05_credencial_acessoModel> Credencial {get; set;} = [];

    public ICollection<tb11_imovelModel> Imovel {get; set;} = [];

    public ICollection<tb08_emailModel> Email {get; set;} = [];
    public tb02_perfilModel Perfil {get; set;} = null!;

}

