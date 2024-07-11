using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Teste_de_conhecimento_Campos_Dealer.Models;

public interface IApiService
{
    Task<List<AddClienteViewModel>> GetClientesAsync();
    Task<List<AddProdutoViewModel>> GetProdutosAsync();
    Task<List<AddVendaViewModel>> GetVendasAsync();
}

public class ApiService : IApiService
{
    private string RemoveEscapeCharacters(string content)
    {
        // Remover os caracteres de escape \/
        return content.Replace("\\/", "/");
    }
    private string ExtractJsonFromXml(string xmlContent)
    {
        // Extrair o conteúdo JSON da string XML
        int start = xmlContent.IndexOf("[");
        int end = xmlContent.LastIndexOf("]");
        if (start >= 0 && end >= 0)
        {
            return xmlContent.Substring(start, end - start + 1);
        }
        else
        {
            throw new ArgumentException("Invalid XML format. Unable to extract JSON.");
        }
    }

    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new CustomDateTimeConverter() }
        };
    }

    public async Task<List<AddClienteViewModel>> GetClientesAsync()
    {

        var response = await _httpClient.GetAsync("https://camposdealer.dev/Sites/TesteAPI/cliente");
        if (response.IsSuccessStatusCode)
        {
            string xmlContent = await response.Content.ReadAsStringAsync();
            string jsonContent = RemoveEscapeCharacters(ExtractJsonFromXml(xmlContent));
            //var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AddClienteViewModel>>(jsonContent, _options);
        }
        return new List<AddClienteViewModel>();
    }

    public async Task<List<AddProdutoViewModel>> GetProdutosAsync()
    {
        var response = await _httpClient.GetAsync("https://camposdealer.dev/Sites/TesteAPI/produto");
        if (response.IsSuccessStatusCode)
        {
            string xmlContent = await response.Content.ReadAsStringAsync();
            string jsonContent = RemoveEscapeCharacters(ExtractJsonFromXml(xmlContent));
            //var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AddProdutoViewModel>>(jsonContent, _options);
        }
        return new List<AddProdutoViewModel>();
    }

    public async Task<List<AddVendaViewModel>> GetVendasAsync()
    {
        var response = await _httpClient.GetAsync("https://camposdealer.dev/Sites/TesteAPI/venda");
        if (response.IsSuccessStatusCode)
        {
            string xmlContent = await response.Content.ReadAsStringAsync();
            string jsonContent = RemoveEscapeCharacters(ExtractJsonFromXml(xmlContent));
            //var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AddVendaViewModel>>(jsonContent, _options);
        }
        return new List<AddVendaViewModel>();
    }
}

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(value.Substring(6, value.Length - 8))).UtcDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"/Date({new DateTimeOffset(value).ToUnixTimeMilliseconds()})/");
    }

}

