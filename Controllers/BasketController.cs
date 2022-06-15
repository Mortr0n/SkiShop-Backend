using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;
        public BasketController(StoreContext context)
        {
            _context = context;
        }

        // Giving the HttpGet a name so that we can return the route in the HttpPost for the 201 CreatedAtRoute
        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();
            return MapBasketToDto(basket);
        }


        [HttpPost] // api/basket?productId=3&quantity=2
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            // get basket || if basket !exist then create basket
            var basket = await RetrieveBasket();
            if (basket == null) basket = CreateBasket();
            // get product
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();
            // add item
            basket.AddItem(product, quantity);
            // save changes
            var result = await _context.SaveChangesAsync() > 0;
            // returning the StatusCode at first.  Later will return the correct item
            // if (result) return StatusCode(201);
            // This is the correct way to return a 201
            if (result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));
            // if result doesn't equal true something has gone wrong.
            return BadRequest(new ProblemDetails{Title = "Problem savving item to basket"});
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            // get basket
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();
            // remove item or reduce quantity
            basket.RemoveItem(productId, quantity);
            // save changes
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();
            return BadRequest(new ProblemDetails{Title = "Problem deleting item from basket"});
        }

        // used extract method to pull this out of the original get to use with other methods
        private async Task<Basket> RetrieveBasket()
        {
            var basket = await _context.Baskets
                // include the items in the basket
                .Include(i => i.Items)
                // make sure to get the product also
                .ThenInclude(p => p.Product)
                // find basket based on buyer ID 
                .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
            return basket;
        }

        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions{ IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            var basket = new Basket{BuyerId = buyerId};
            _context.Baskets.Add(basket);
            return basket;
        }

        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Brand = item.Product.Brand,
                    Type = item.Product.Type,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}