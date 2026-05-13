using System;
using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.FavoritoUseCase.DTOs;

public class AdicionarFavoritoDTO
{
    [Required(ErrorMessage = "Informe o ID do cliente.")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "Informe o ID do imóvel.")]
    public int IdImovel { get; set; }
}

