﻿using Microsoft.AspNetCore.Mvc;
using Nest;
using Notification.Application.Models;

namespace Notification.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IElasticClient _elasticClient;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IElasticClient elasticClient, ILogger<ProductsController> logger)
    {
        _elasticClient = elasticClient;
        _logger = logger;
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> Get(string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
            return BadRequest("Keyword cannot be empty");

        var results = await _elasticClient.SearchAsync<Product>(
            s => s.Query(
                q => q.QueryString(
                    d => d.Query('*' + keyword + '*')
                )
            ).Size(1000)
        );

        return Ok(results.Documents.ToList());
    }

    [HttpPost(Name = "AddProduct")]
    public async Task<IActionResult> Post(Product product)
    {
        await _elasticClient.IndexDocumentAsync(product);
        return Ok();
    }

    [HttpPost(Name = "UpdateProduct")]
    public async Task<IActionResult> Update(Product product)
    {
        await _elasticClient.UpdateAsync<Product>(product.Id, u => u.Doc(product));
        return Ok();
    }

    [HttpPost(Name = "DeleteProduct")]
    public async Task<IActionResult> Delete(int id)
    {
        await _elasticClient.DeleteAsync<Product>(id);
        return Ok();
    }
}