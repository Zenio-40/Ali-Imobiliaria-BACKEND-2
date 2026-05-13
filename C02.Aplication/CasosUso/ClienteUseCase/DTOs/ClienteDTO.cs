using System;

namespace Corretora.C02.Aplication.CasosUso.ClienteUseCase.DTOs;

public class ClienteDTO
{
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; } = String.Empty;
    public string ClienteTelefone { get; set; } = String.Empty;
    public string ClienteEmail { get; set; } = String.Empty;
    public bool ClienteEstado { get; set; }
    public int ClienteIdPerfil { get; set; }
    public string ClientePerfil { get; set; } = String.Empty;
}

