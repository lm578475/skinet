using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase  //API json responses, but not views (MVC withoout the View, this will be done with Angular)
{
    private readonly StoreContext context;

    public ProductsController(StoreContext context){
        this.context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(){

       //testing git
        return await context.Products.ToListAsync();

    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id){

        var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product){
        context.Products.Add(product);

        await context.SaveChangesAsync();
        
        return product;
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id){

        var product = await context.Products.FindAsync(id);
        if (product == null) 
            return NotFound();
        
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product){
        if (!ProductExists(id) || product.Id != id){
            return NotFound();
        }

        context.Entry(product).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id){
        return context.Products.Any(x=> x.Id == id);
    }
    
}


