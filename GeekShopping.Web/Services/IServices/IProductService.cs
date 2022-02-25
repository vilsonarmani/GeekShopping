namespace GeekShopping.Web.Models.Services.IServices;

public interface IProductService
{
    public Task<IEnumerable<ProductModel>> FindAllProducts();
    public Task<ProductModel> FindProductById(long id);
    public Task<ProductModel> CreateProduct(ProductModel model);
    public Task<ProductModel> UpdateProduct(ProductModel model);
    public Task<bool> DeleteProductById(long id);


}

