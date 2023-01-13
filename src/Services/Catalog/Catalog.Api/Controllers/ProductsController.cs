using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _productRepository.GetProducts());
    }

    [HttpGet("GetByIds")]
    [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsByIds([FromQuery] string ids)
    {
        return Ok(await _productRepository.GetProducts(ids.Split(',')));
    }

    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(string id)
    {
        var product = await _productRepository.GetProduct(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpGet("ByCategory/{categoryName}")]
    [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductByCategory(string categoryName)
    {
        var products = await _productRepository.GetProductsByCategoryName(categoryName);
        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        var createdProduct = await _productRepository.CreateProduct(product);

        return CreatedAtRoute("GetProductById", new { id = product.Id }, createdProduct);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct(Product product)
    {
        var result = await _productRepository.UpdateProduct(product);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var result = await _productRepository.DeleteProduct(id);

        return Ok(result);
    }
}