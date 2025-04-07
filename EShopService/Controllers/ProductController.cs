using EShop.Domain;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace EShopService.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{

    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    // GET api/<ProductController>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _service.GetById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
    // POST api/<ProductController>
    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        _service.AddProduct(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    // PUT api/<ProductController>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Product product)
    {
        if (id != product.Id) return BadRequest();
        _service.UpdateProduct(product);
        return NoContent();
    }

    // DELETE api/<ProductController>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.DeleteProduct(id);
        return NoContent();
    }

    // EXISTS api/<ProductController>
    [HttpGet("exists/{id}")]
    public ActionResult<bool> Exists(int id)
    {
        var exists = _service.GetById(id) != null;
        return Ok(exists);
    }
}
