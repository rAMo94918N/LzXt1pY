// 代码生成时间: 2025-09-24 00:38:45
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 定义一个简单的模型
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// 定义一个存储产品的服务
public class ProductService
{
    private static readonly List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Price = 100m },
        new Product { Id = 2, Name = "Product 2", Price = 200m },
        new Product { Id = 3, Name = "Product 3", Price = 300m }
    };

    public IEnumerable<Product> GetAllProducts()
    {
        return products;
    }

    public Product GetProductById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) throw new KeyNotFoundException("Product not found.");
        return product;
    }

    public Product CreateProduct(Product product)
    {
        products.Add(product);
        return product;
    }

    public Product UpdateProduct(Product product)
    {
        var existingProduct = GetProductById(product.Id);
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        return existingProduct;
    }

    public void DeleteProduct(int id)
    {
        var product = GetProductById(id);
        products.Remove(product);
    }
}

// 创建一个控制器处理API请求
[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    // 获取所有产品
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // 根据ID获取单个产品
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var product = _productService.GetProductById(id);
            return Ok(product);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // 创建新产品
    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        try
        {
            var createdProduct = _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // 更新产品信息
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Product product)
    {
        try
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }
            var updatedProduct = _productService.UpdateProduct(product);
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // 删除产品
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _productService.DeleteProduct(id);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
