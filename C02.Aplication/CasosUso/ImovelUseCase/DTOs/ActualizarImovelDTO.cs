using System.ComponentModel.DataAnnotations;

namespace Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;

public class ActualizarImovelDTO
{
    [Required(ErrorMessage = "Informe o ID do imovel.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe a descricao do imovel.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o preco do imovel.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Preco deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "Selecione o tipo de imovel.")]
    public int IdTipoImovel { get; set; }

    [Required(ErrorMessage = "Selecione a tipologia.")]
    public int IdTipologia { get; set; }

    [Required(ErrorMessage = "Selecione o funcionario responsavel.")]
    public int IdFuncionario { get; set; }

    public int? IdBairro { get; set; }

    public int? IdMunicipio { get; set; }

    public int? IdProvincia { get; set; }

    public string Endereco { get; set; } = string.Empty;

    public List<string> Fotos { get; set; } = [];

    public string? VideoUrl { get; set; }

    public bool Estado { get; set; } = true;

    public string EstadoAprovacao { get; set; } = "Pendente";
}
