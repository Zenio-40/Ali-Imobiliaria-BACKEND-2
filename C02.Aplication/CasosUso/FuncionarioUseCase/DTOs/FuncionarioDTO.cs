using System;

namespace Corretora.C02.Aplication.CasosUso.FuncionarioUseCase.DTOs;

public class FuncionarioDTO
{
public int FuncionarioId { get; set; }
    public string FuncionarioNome { get; set; } = String.Empty;
    public string FuncionarioTelefone { get; set; } = String.Empty;
    public string FuncionarioNif { get; set; } = String.Empty;
    public bool FuncionarioEstado { get; set; }
    public int FuncionarioIdPerfil { get; set; }
    public string FuncionarioPerfil { get; set; } = String.Empty;
}
