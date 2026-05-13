using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Corretora.C02.Aplication.Servico.ISmsService;

namespace Corretora.C03.Infra.Servico.ISmsService;

public class SmsService(HttpClient httpClient) : Corretora.C02.Aplication.Servico.ISmsService.ISmsService
{
    private const string Endpoint = "https://sms.gsaplatform.co/enviar/sms";

    public async Task<bool> EnviarSmsAsync(string numero, string mensagem, string nif)
{
    var request = new SmsRequest
    {
        Mensagem = [new SmsItem { Telefone = numero, MensagemTexto = mensagem }],
        Nif = nif
    };

    var response = await httpClient.PostAsJsonAsync(Endpoint, request);
    
    // ✅ Adicione isto para ver o erro exato
    var responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"SMS Status: {response.StatusCode}");
    Console.WriteLine($"SMS Response: {responseBody}");
    Console.WriteLine($"SMS Telefone: {numero}");
    Console.WriteLine($"SMS Nif: {nif}");
    
    return response.IsSuccessStatusCode;
}

    private sealed class SmsRequest
    {
        [JsonPropertyName("mensagem")]
        public List<SmsItem> Mensagem { get; set; } = [];

        [JsonPropertyName("nif")]
        public string Nif { get; set; } = string.Empty;
    }

    private sealed class SmsItem
    {
        [JsonPropertyName("telefone")]
        public string Telefone { get; set; } = string.Empty;

        [JsonPropertyName("mensagemTexto")]
        public string MensagemTexto { get; set; } = string.Empty;
    }
}
