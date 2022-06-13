using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
    
        public ProductsController(StoreContext context)
        {       
            _context = context;
        }

         [HttpGet] // api/products as a Get
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();

            // can do it this way or return the products instead
            // var products = await context.Products.ToListAsync();
            // return Ok(products);
        }

        [HttpGet("{id}")] // api/products/id  as a Get
        public async Task<ActionResult<Product>>GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Synchronous way of doing this
        // [HttpGet] // api/products as a Get
        // public ActionResult<List<Product>> GetProducts()
        // {
        //     var products = context.Products.ToList();

        //     return Ok(products);
        // }

        // [HttpGet("{id}")] // api/products/id  as a Get
        // public ActionResult<Product> GetProduct(int id)
        // {
        //     return context.Products.Find(id);
        // }
    }
}