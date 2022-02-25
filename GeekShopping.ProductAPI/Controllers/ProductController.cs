using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    enum Operacao
    {
        Create,
        Update
    }
    private IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository ?? throw new
            ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVO>>> FindByAll()
    {
        var products = await _repository.FindAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVO>> FindById(long id)
    {
        var product = await _repository.FindById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductVO>> Create( [FromBody] ProductVO vo)
    {
        return await OperacaoUnicaProduct(vo, Operacao.Create);
    }

    private async Task<ActionResult<ProductVO>> OperacaoUnicaProduct(ProductVO vo, Operacao operacao)
    {
        if (vo == null) return BadRequest();

        ProductVO product = new ProductVO();


        switch (operacao)
        {
            case Operacao.Create:
                product = await _repository.Create(vo);
                break;
            case Operacao.Update:
                product = await _repository.Update(vo);
                break;
        }


        return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO vo)
    {
        return await OperacaoUnicaProduct(vo,Operacao.Update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        var status = await _repository.Delete(id);
        if (!status) return BadRequest();
        return Ok(status);
    }

}
