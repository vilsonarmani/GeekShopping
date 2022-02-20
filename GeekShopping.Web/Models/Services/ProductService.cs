using GeekShopping.Web.Models.Services.IServices;
using GeekShopping.Web.Utils;


namespace GeekShopping.Web.Models.Services;
public class ProductService : IProductService
{
    public const string ERROR_MESSAGE_CALLING_API = "Something went wrong when calling API";
    //utilizar clientfactory
    // interface responsavel por requisições em aspnet
    //injetar
    private readonly HttpClient _client;
    public const string BasePath = "api/v1/product";

    public ProductService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<ProductModel>> FindAllProducts()
    {
        var response = await _client.GetAsync(BasePath);
        //utilizo aqui a extension method
        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> FindProductById(long id)
    {
        var response = await _client.GetAsync($"{BasePath}/{id}");
        //utilizo aqui a extension method
        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> CreateProduct(ProductModel model)
    {
        //faz o post e retorna a response
        var response = await _client.PostAsJson(BasePath, model);
        //pega a response e 
        if (response.IsSuccessStatusCode) //200 / 204 / 201 etc
            return await response.ReadContentAs<ProductModel>();
        else
            throw new Exception(ERROR_MESSAGE_CALLING_API);
    }
    public async Task<ProductModel> UpdateProduct(ProductModel model)
    {
        //faz o post e retorna a response
        var response = await _client.PutAsJson(BasePath, model);
        //pega a response e 
        if (response.IsSuccessStatusCode) //200 / 204 / 201 etc
            return await response.ReadContentAs<ProductModel>();
        else
            throw new Exception(ERROR_MESSAGE_CALLING_API);
    }
    public async Task<bool> DeleteProductById(long id)
    {
        var response = await _client.DeleteAsync($"{BasePath}/{id}");
        //utilizo aqui a extension method
        if (response.IsSuccessStatusCode)             
        return await response.ReadContentAs<bool>();
        else
            throw new Exception(ERROR_MESSAGE_CALLING_API);
    }
}

