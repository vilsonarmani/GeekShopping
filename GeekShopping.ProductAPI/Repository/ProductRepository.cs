using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{

    private readonly ProductContext _context;
    private IMapper _mapper;

    public ProductRepository(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductVO>> FindAll()
    {
        
        List<Product> products = await _context.Products.ToListAsync();

        return _mapper.Map<List<ProductVO>>(products);
    }

    public async Task<ProductVO> FindById(long id)
    {
        Product product =
            await _context.Products.Where(prod => prod.Id == id)
            .FirstOrDefaultAsync();
        return _mapper.Map<ProductVO>(product);
    }    
    public async Task<ProductVO> Create(ProductVO vo)
    {

        Product product = _mapper.Map<Product>(vo);//mapeia o vo recebido. já pé um produto e precisa persistir
        _context.Products.Add(product); //adiciona.

        await _context.SaveChangesAsync(); // salva o produto no BD 

        return _mapper.Map<ProductVO>(product); // converto novamente pra View (entra VO e sai VO)
    }
    public async Task<ProductVO> Update(ProductVO vo)
    {
        Product product = _mapper.Map<Product>(vo);//mapeia o vo recebido. já é um produto e precisa persistir
        _context.Products.Update(product); //adiciona.

        await _context.SaveChangesAsync(); // salva o produto no BD 

        return _mapper.Map<ProductVO>(product); // converto novamente pra View (entra VO e sai VO)
    }
    public async Task<bool> Delete(long id)
    {
        try
        {  

            ///caminho feliz
            Product product =
            await _context.Products.Where(prod => prod.Id == id)
                                   .FirstOrDefaultAsync();

            if (product == null) return false;

            _context.Remove(product);
            await _context.SaveChangesAsync(true);
            return true;

        }
        catch (Exception)
        {
            return false;
        }

        
    }
}