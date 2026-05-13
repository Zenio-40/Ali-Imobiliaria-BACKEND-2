using System;

namespace Corretora.C02.Aplication.CasosUso.ProprietarioUseCase.DTOs;

public class ProprietarioDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = String.Empty;
    public string Telefone { get; set; } = String.Empty;
    public int IdTipoImovel { get; set; }
    public string TipoImovel { get; set; } = String.Empty;
}

