using System.Net.Http.Headers;
namespace GeekShopping.Web.Utils;

public static class HTTPClientExtensions
{
    private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) throw
                 new ApplicationException(
                     $"Something went wrong calling the API: " +
                     $"{response.ReasonPhrase}");
        //convert json para objeto
        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(
            dataAsString, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
    public static Task<HttpResponseMessage> PostAsJson<T>(
        this HttpClient httpClient, 
        string url,
        T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        //montar a request
        var content = new StringContent(dataAsString);
        //setar algumas propriedades enviadas na requisição
        content.Headers.ContentType = contentType;
        // retornar um http cliente 
        return httpClient.PostAsync(url, content);

    }

    public static Task<HttpResponseMessage> PutAsJson<T>(
        this HttpClient httpClient,
        string url,
        T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        //montar a request
        var content = new StringContent(dataAsString);
        //setar algumas propriedades enviadas na requisição
        content.Headers.ContentType = contentType;
        // retornar um http cliente 
        return httpClient.PutAsync(url, content);

    }

}

