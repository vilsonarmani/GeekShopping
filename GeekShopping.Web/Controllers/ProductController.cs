using GeekShopping.Web.Models;
using GeekShopping.Web.Models.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    public async Task<IActionResult> ProductIndex()
    {
        var products = await _productService.FindAllProducts();
        return View(products);
    }
    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.CreateProduct(model);
            if (response != null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }

    public async Task<IActionResult> ProductUpdate(int id)
    {

        var model = await _productService.FindProductById(id);

        if (model != null) return View(model);

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProduct(model);
            if (response != null) return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }

    public async Task<IActionResult> ProductDelete(int id) //Metodo que pega a rota da view (grid) e manda pra ProductDelete.cshtml
    {
        var model = await _productService.FindProductById(id);
        if (model != null) return View(model);
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductModel model) // méthod called by the form-action (it's using the verb post)
    {
        var response = await _productService.DeleteProductById(model.Id);
        if (response) return RedirectToAction(nameof(ProductIndex));

        return View(model);
    }


}

