namespace Corretora.C02.Aplication.CasosUso.ImovelUseCase.DTOs;

public class ImovelDTO
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public bool Estado { get; set; }
    public string EstadoAprovacao { get; set; } = "Pendente";
    public int IdTipoImovel { get; set; }
    public string TipoImovel { get; set; } = string.Empty;
    public int IdTipologia { get; set; }
    public string Tipologia { get; set; } = string.Empty;
    public int IdFuncionario { get; set; }
    public string Funcionario { get; set; } = string.Empty;
    public int? IdProprietario { get; set; }
    public string Proprietario { get; set; } = string.Empty;
    public List<string> Fotos { get; set; } = [];
    public string? VideoUrl { get; set; }
    public int? IdProvincia { get; set; }
    public string Provincia { get; set; } = string.Empty;
    public int? IdMunicipio { get; set; }
    public string Municipio { get; set; } = string.Empty;
    public int? IdBairro { get; set; }
    public string Bairro { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
}
